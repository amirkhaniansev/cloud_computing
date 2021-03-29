using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivescoreDAL.Base;
using LivescoreDAL.Constants;
using LivescoreDAL.Database;
using LivescoreDAL.Description;
using LivescoreDAL.Parameters;

namespace LivescoreDAL.Factories
{
    public class DalFactory : Disposable, IDalFactory
    {
        private readonly DatabaseConfiguration configuration;
        
        private IBaseDAL baseDAL;
        private Dictionary<string, IBaseDAL> dals;
        private Dictionary<string, Func<DatabaseConfiguration, IBaseDAL, IBaseDAL>> initializers;
        
        public DalFactory(DatabaseConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IMatchDAL GetMatchDAL()
        {
            return this.GetDAL(DALs.MatchDAL) as IMatchDAL;
        }

        public ISeasonDAL GetSeasonDAL()
        {
            return this.GetDAL(DALs.SeasonDAL) as ISeasonDAL;
        }

        public ISportDAL GetSportDAL()
        {
            return this.GetDAL(DALs.SportDAL) as ISportDAL;
        }

        public ITeamDAL GetTeamDAL()
        {
            return this.GetDAL(DALs.TeamDAL) as ITeamDAL;
        }

        protected override void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            if (disposing)
            {
                foreach (var dal in this.dals.Values)
                    dal.Dispose();

                this.baseDAL.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override async ValueTask DisposeAsyncCore()
        {
            foreach (var dal in this.dals.Values)
                await dal.DisposeAsync();

            await this.baseDAL.DisposeAsync();
            await base.DisposeAsync();
        }

        private IBaseDAL GetDAL(string name)
        {
            if (this.dals.TryGetValue(name, out var dal))
                return dal;

            if (this.initializers == null)
            {
                this.initializers = new Dictionary<string, Func<DatabaseConfiguration, IBaseDAL, IBaseDAL>>
                {
                    [DALs.MatchDAL] = (c, d) => new MatchDAL(c, d),
                    [DALs.SeasonDAL] = (c, d) => new SeasonDAL(c, d),
                    [DALs.SportDAL] = (c, d) => new SportDAL(c, d),
                    [DALs.TeamDAL] = (c, d) => new TeamDAL(c, d)
                };
            }

            var initializer = this.initializers[name];
            if (this.baseDAL == null)
                this.baseDAL = initializer.Invoke(this.configuration, null);

            return this.dals[name] = initializer.Invoke(this.configuration, this.baseDAL);
        }
    }
}