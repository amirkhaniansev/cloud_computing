using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LivescoreAPI.Constants;
using LivescoreAPI.Response;
using LivescoreDAL.Factories;
using LivescoreDAL.Filters;
using LivescoreDAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace LivescoreAPI.Controllers
{
    [Route(Routes.Country)]
    public class CountryController : LivescoreSwaggerController
    {
        public CountryController(DalFactory factory) : base(factory)
        {
        }

        [HttpGet(Parameters.Id)]
        public async Task<Country> Get(int id)
        {
            using var sportDal = this.factory.GetSportDAL();
            var country = await sportDal.Find<Country>(id);
            if (country == null)
                throw this.NotFound(Errors.CountryNotFound);

            return country;
        }

        [HttpGet]
        public async Task<List<Country>> Get([FromQuery]CountrySearcher filter)
        {
            using var sportDal = this.factory.GetSportDAL();
            var countries = await sportDal.GetCountries(filter);
            if (countries == null || countries.Count == 0)
                throw this.NotFound(Errors.CountryNotFound);

            return countries;
        }

        [HttpPost]
        public async Task<Country> Post([FromBody] Country country)
        {
            using var sportDal = this.factory.GetSportDAL();

            country.Created = DateTime.Now;
            country.Modified = DateTime.Now;

            var filter = new CountrySearcher { Name = country.Name };
            var countries = await sportDal.GetCountries(filter);
            if (countries != null && countries.Count > 0 && countries.Any(c => c.Name == country.Name))
                throw this.BadRequest(Errors.CountryExists);

            var newCountry = await sportDal.AddCountry(country);
            if (newCountry == null)
                throw this.BadRequest(Errors.InvalidModel);

            await sportDal.Save();

            return newCountry;
        }

        [HttpPut]
        public async Task<Country> Put([FromBody]Country country)
        {
            using var sportDal = this.factory.GetSportDAL();
            var oldCountry = await sportDal.Find<Country>(country.Id);
            if (oldCountry == null)
                throw this.NotFound(Errors.CountryNotFound);

            oldCountry.Modified = DateTime.Now;
            oldCountry.Name = country.Name;
            oldCountry.FlagURL = country.FlagURL;

            sportDal.UpdateCountry(country);

            await sportDal.Save();

            return oldCountry;
        }

        [HttpDelete(Parameters.Id)]
        public async Task<ApiResponse> Delete(int id)
        {
            using var sportDal = this.factory.GetSportDAL();
            var country = await sportDal.Find<Country>(id);
            if (country == null)
                throw this.NotFound(Errors.CountryNotFound);

            sportDal.DeleteCountry(country);

            await sportDal.Save();

            return this.Success(new ApiResponse());
        }
    }
}
