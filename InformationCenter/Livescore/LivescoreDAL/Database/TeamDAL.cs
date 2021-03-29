using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LivescoreDAL.Description;
using LivescoreDAL.Filters;
using LivescoreDAL.Models;
using LivescoreDAL.Parameters;
using LivescoreDAL.Base;

namespace LivescoreDAL.Database
{
    internal class TeamDAL : BaseDAL, ITeamDAL
    {
        public TeamDAL(DatabaseConfiguration configuration, IBaseDAL parent = null) 
            : base(configuration, parent)
        {
        }

        public Task<Player> AddPlayer(Player player)
        {
            return this.Add(player);
        }

        public Task<Team> AddTeam(Team team)
        {
            return this.Add(team);
        }

        public void DeletePlayer(Player player)
        {
            this.Delete(player);
        }

        public void DeleteTeam(Team team)
        {
            this.Delete(team);
        }

        public Task<List<Player>> GetPlayers(PlayerSearcher filter)
        {
            return this.GetContext()
                       .Players
                       .AsQueryable()
                       .Filter(filter)
                       .ToListAsync();
        }

        public Task<List<Team>> GetTeams(TeamSearcher filter)
        {
            return this.GetContext()
                       .Teams
                       .AsQueryable()
                       .Filter(filter)
                       .ToListAsync();
        }

        public void UpdatePlayer(Player player)
        {
            this.Update(player);
        }

        public void UpdateTeam(Team team)
        {
            this.Update(team);
        }
    }
}
