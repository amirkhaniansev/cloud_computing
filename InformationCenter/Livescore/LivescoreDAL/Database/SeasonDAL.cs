using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LivescoreDAL.Base;
using LivescoreDAL.Description;
using LivescoreDAL.Filters;
using LivescoreDAL.Models;
using LivescoreDAL.Parameters;

namespace LivescoreDAL.Database
{
    internal class SeasonDAL : BaseDAL, ISeasonDAL
    {
        public SeasonDAL(DatabaseConfiguration configuration, IBaseDAL parent = null) 
            : base(configuration, parent)
        {
        }

        public Task<Season> AddSeason(Season season)
        {
            return this.Add(season);
        }

        public Task<SeasonTeam> AddSeasonTeam(SeasonTeam seasonTeam)
        {
            return this.Add(seasonTeam);
        }

        public void DeleteSeason(Season season)
        {
            this.Delete(season);
        }

        public void DeleteSeasonTeam(SeasonTeam seasonTeam)
        {
            this.Delete(seasonTeam);
        }

        public Task<List<Season>> GetSeasons(SeasonSearcher filter)
        {
            return this.GetContext()
                       .Seasons
                       .AsQueryable()
                       .Filter(filter)
                       .ToListAsync();
        }

        public Task<List<SeasonTeam>> GetSeasonTeams(SeasonTeamSearcher filter)
        {
            return this.GetContext()
                       .SeasonTeams
                       .AsQueryable()
                       .Filter(filter)
                       .ToListAsync();
        }

        public void UpdateSeason(Season season)
        {
            this.UpdateSeason(season);
        }

        public void UpdateSeasonTeam(SeasonTeam seasonTeam)
        {
            this.UpdateSeasonTeam(seasonTeam);
        }
    }
}