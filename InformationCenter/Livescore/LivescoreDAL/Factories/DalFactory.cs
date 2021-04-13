using LivescoreDAL.Base;
using LivescoreDAL.Database;
using LivescoreDAL.Description;
using LivescoreDAL.Parameters;

namespace LivescoreDAL.Factories
{
    public class DalFactory : Disposable, IDalFactory
    {
        private readonly DatabaseConfiguration configuration;

        public DalFactory(DatabaseConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IMatchDAL GetMatchDAL()
        {
            return new MatchDAL(this.configuration);
        }

        public IMatchDAL GetMatchDAL(IBaseDAL parent)
        {
            return new MatchDAL(this.configuration, parent);
        }

        public ISeasonDAL GetSeasonDAL()
        {
            return new SeasonDAL(this.configuration);
        }

        public ISeasonDAL GetSeasonDAL(IBaseDAL parent)
        {
            return new SeasonDAL(this.configuration, parent);
        }

        public ISportDAL GetSportDAL()
        {
            return new SportDAL(this.configuration);
        }

        public ISportDAL GetSportDAL(IBaseDAL parent)
        {
            return new SportDAL(this.configuration, parent);
        }

        public ITeamDAL GetTeamDAL()
        {
            return new TeamDAL(this.configuration);
        }

        public ITeamDAL GetTeamDAL(IBaseDAL parent)
        {
            return new TeamDAL(this.configuration, parent);
        }
    }
}