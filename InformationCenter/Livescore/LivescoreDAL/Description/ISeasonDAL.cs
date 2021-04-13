using System.Collections.Generic;
using System.Threading.Tasks;
using LivescoreDAL.Filters;
using LivescoreDAL.Models;

namespace LivescoreDAL.Description
{
    public interface ISeasonDAL : IBaseDAL
    {
        Task<Season> AddSeason(Season season);

        Task<List<Season>> GetSeasons(SeasonSearcher filter);

        void UpdateSeason(Season season);

        void DeleteSeason(Season season);

        Task<SeasonTeam> AddSeasonTeam(SeasonTeam seasonTeam);

        Task<List<SeasonTeam>> GetSeasonTeams(SeasonTeamSearcher filter);

        void UpdateSeasonTeam(SeasonTeam seasonTeam);

        void DeleteSeasonTeam(SeasonTeam seasonTeam);
    }
}
