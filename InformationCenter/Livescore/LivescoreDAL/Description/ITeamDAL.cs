using System.Threading.Tasks;
using System.Collections.Generic;
using LivescoreDAL.Filters;
using LivescoreDAL.Models;

namespace LivescoreDAL.Description
{
    public interface ITeamDAL : IBaseDAL
    {
        Task<List<Team>> GetTeams(TeamSearcher filter);

        Task<Team> AddTeam(Team team);

        void UpdateTeam(Team team);

        void DeleteTeam(Team team);

        Task<List<Player>> GetPlayers(PlayerSearcher filter);

        Task<Player> AddPlayer(Player player);

        void UpdatePlayer(Player player);

        void DeletePlayer(Player player);
    }
}