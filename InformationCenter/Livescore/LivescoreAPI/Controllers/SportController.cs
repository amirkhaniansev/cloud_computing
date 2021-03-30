using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivescoreAPI.Constants;
using LivescoreAPI.Response;
using LivescoreDAL.Factories;
using LivescoreDAL.Filters;
using LivescoreDAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace LivescoreAPI.Controllers
{
    [Route(Routes.Sport)]
    public class SportController : LivescoreSwaggerController
    {
        public SportController(DalFactory factory) : base(factory)
        {
        }

        [HttpGet(Parameters.Id)]
        public async Task<Sport> Get(int id)
        {
            using var sportDal = this.factory.GetSportDAL();
            var sport = await sportDal.Find<Sport>(id);
            if (sport == null)
                throw this.NotFound(Errors.SportNotFound);

            return sport;
        }

        [HttpGet]
        public async Task<List<Sport>> Get([FromQuery]SportSearcher filter)
        {
            using var sportDal = this.factory.GetSportDAL();
            var sports = await sportDal.GetSports(filter);
            if (sports == null || sports.Count == 0)
                throw this.NotFound(Errors.SportNotFound);

            return sports;
        }

        [HttpPost]
        public async Task<Sport> Post([FromBody]Sport sport)
        {
            using var sportDal = this.factory.GetSportDAL();
            if (sport.OriginCountryId.HasValue)
            {
                var originCountry = await sportDal.Find<Sport>(sport.OriginCountryId.Value);
                if (originCountry == null)
                    throw this.NotFound(Errors.CountryNotFound);
            }

            sport.Created = DateTime.Now;
            sport.Modified = DateTime.Now;

            var newSport = await sportDal.AddSport(sport);
            if (newSport == null)
                throw this.BadRequest(Errors.InvalidModel);

            await sportDal.Save();

            return newSport;
        }

        [HttpPut]
        public async Task<Sport> Put([FromBody]Sport sport)
        {
            using var sportDal = this.factory.GetSportDAL();
            var oldSport = await sportDal.Find<Sport>(sport.Id);
            if (oldSport == null)
                throw this.NotFound(Errors.SportNotFound);

            if (sport.OriginCountryId.HasValue)
            {
                var originCountry = await sportDal.Find<Sport>(sport.OriginCountryId.Value);
                if (originCountry == null)
                    throw this.NotFound(Errors.CountryNotFound);
            }

            oldSport.Modified = DateTime.Now;
            oldSport.LogoURL = sport.LogoURL;
            oldSport.Name = sport.Name;
            oldSport.OriginCountryId = sport.OriginCountryId;

            sportDal.UpdateSport(sport);

            await sportDal.Save();

            return sport;
        }

        [HttpDelete(Parameters.Id)]
        public async Task<ApiResponse> Delete(int id)
        {
            using var sportDal = this.factory.GetSportDAL();
            var sport = await sportDal.Find<Sport>(id);
            if (sport == null)
                throw this.NotFound(Errors.SportNotFound);

            sportDal.DeleteSport(sport);

            await sportDal.Save();

            return this.Success(new ApiResponse());
        }
    }
}