using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using LivescoreAPI.Constants;
using LivescoreDAL.Factories;
using LivescoreDAL.Filters;
using LivescoreDAL.Models;
using LivescoreAPI.Response;

namespace LivescoreAPI.Controllers
{
    [Route(Routes.Competition)]
    public class CompetitionController : LivescoreSwaggerController
    {
        public CompetitionController(DalFactory factory) : base(factory)
        {
        }

        [HttpGet(Parameters.Id)]
        public async Task<Competition> Get(int id)
        {
            if (id < 0)
                throw this.BadRequest(Errors.InvalidId);

            using var sportDal = this.factory.GetSportDAL();
            var competition = await sportDal.Find<Competition>(id);
            if (competition == null)
                throw this.NotFound(Errors.CompetitionNotFound);

            return competition;
        }

        [HttpGet]
        public async Task<List<Competition>> Get([FromQuery] CompetitionSearcher filter)
        {
            using var dal = this.factory.GetSportDAL();
            var competitions = await dal.GetCompetitions(filter);
            if (competitions == null || competitions.Count == 0)
                throw this.NotFound(Errors.CompetitionNotFound);

            return competitions;
        }

        [HttpPost]
        public async Task<Competition> Post([FromBody] Competition competition)
        {
            using var sportDal = this.factory.GetSportDAL();
            var country = await sportDal.Find<Country>(competition.CountryId);
            if (country == null)
                throw this.NotFound(Errors.CountryNotFound);

            var sport = await sportDal.Find<Sport>(competition.SportId);
            if (sport == null)
                throw this.NotFound(Errors.SportNotFound);

            competition.Created = DateTime.Now;
            competition.Modified = DateTime.Now;

            var newCompetition = await sportDal.AddCompetition(competition);
            if (newCompetition == null)
                throw this.BadRequest(Errors.InvalidModel);

            await sportDal.Save();

            return newCompetition;
        }

        [HttpPut]
        public async Task<Competition> Put([FromBody] Competition competition)
        {
            using var sportDal = this.factory.GetSportDAL();
            var uCompetition = await sportDal.Find<Competition>(competition.Id);
            if (uCompetition == null)
                throw this.NotFound(Errors.CompetitionNotFound);

            uCompetition.Modified = DateTime.Now; 
            var country = await sportDal.Find<Country>(competition.CountryId);
            if (country == null)
                throw this.NotFound(Errors.CountryNotFound);

            var sport = await sportDal.Find<Sport>(competition.SportId);
            if (sport == null)
                throw this.NotFound(Errors.SportNotFound);

            uCompetition.Name = competition.Name;
            uCompetition.LogoURL = competition.LogoURL;
            uCompetition.CountryId = competition.CountryId;
            uCompetition.SportId = competition.SportId;

            sportDal.UpdateCompetition(uCompetition);

            await sportDal.Save();

            return uCompetition;
        }

        [HttpDelete(Parameters.Id)]
        public async Task<ApiResponse> Delete(int id)
        {
            using var sportDal = this.factory.GetSportDAL();
            var competition = await sportDal.Find<Competition>(id);
            if (competition == null)
                throw this.NotFound(Errors.CompetitionNotFound);

            sportDal.DeleteCompetition(competition);

            await sportDal.Save();

            return this.Success(new ApiResponse());
        }
    }
}