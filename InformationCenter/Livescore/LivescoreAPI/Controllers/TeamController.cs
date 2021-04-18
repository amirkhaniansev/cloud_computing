using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivescoreAPI.Constants;
using LivescoreDAL.Factories;
using LivescoreDAL.Filters;
using LivescoreDAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace LivescoreAPI.Controllers
{
    [Route(Routes.Team)]
    public class TeamController : LivescoreSwaggerController
    {
        public TeamController(DalFactory factory) : base(factory)
        {
        }

        [HttpGet(Parameters.Id)]
        public async Task<Team> Get(int id)
        {
            using var teamDal = this.factory.GetTeamDAL();
            var team = await teamDal.Find<Team>(id);
            if (team == null)
                throw this.NotFound(Errors.TeamNotFound);

            return team;
        }

        [HttpGet]
        public async Task<List<Team>> Get([FromQuery]TeamSearcher filter)
        {
            using var teamDal = this.factory.GetTeamDAL();
            var teams = await teamDal.GetTeams(filter);
            if (teams == null || teams.Count == 0)
                throw this.NotFound(Errors.TeamNotFound);

            return teams;
        }

        [HttpPost]
        public async Task<Team> Post([FromBody]Team team)
        {
            using var teamDal = this.factory.GetTeamDAL();
            using var sportDal = this.factory.GetSportDAL(teamDal);
            var country = await sportDal.Find<Country>(team.CountryId);
            if (country == null)
                throw this.NotFound(Errors.CountryNotFound);

            var sport = await sportDal.Find<Sport>(team.SportId);
            if (sport == null)
                throw this.NotFound(Errors.SportNotFound);

            team.Created = DateTime.Now;
            team.Modified = DateTime.Now;

            var newTeam = await teamDal.AddTeam(team);
            if (newTeam == null)
                throw this.BadRequest(Errors.InvalidModel);

            await teamDal.Save();

            return newTeam;
        }
    }
}
