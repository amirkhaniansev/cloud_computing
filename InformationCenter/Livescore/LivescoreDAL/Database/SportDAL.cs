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
    internal class SportDAL : BaseDAL, ISportDAL
    {
        public SportDAL(DatabaseConfiguration configuration, IBaseDAL parent = null) 
            : base(configuration, parent)
        {
        }

        public Task<Country> AddCountry(Country country)
        {
            return this.Add(country);
        }

        public Task<Sport> AddSport(Sport sport)
        {
            return this.Add(sport);
        }

        public void DeleteCompetition(Competition competition)
        {
            this.Delete(competition);
        }

        public void DeleteCountry(Country country)
        {
            this.Delete(country);
        }

        public void DeleteSport(Sport sport)
        {
            this.Delete(sport);
        }

        public Task<List<Competition>> GetCompetitions(CompetitionSearcher filter)
        {
            return this.GetContext()
                       .Competitions
                       .AsQueryable()
                       .Filter(filter)
                       .ToListAsync();
        }

        public Task<List<Country>> GetCountries(CountrySearcher filter)
        {
            return this.GetContext()
                       .Countries
                       .AsQueryable()
                       .Filter(filter)
                       .ToListAsync();
        }

        public Task<List<Sport>> GetSports(SportSearcher filter)
        {
            return this.GetContext()
                       .Sports
                       .AsQueryable()
                       .Filter(filter)
                       .ToListAsync();
        }

        public void UpdateCompetition(Competition competition)
        {
            this.Update(competition);
        }

        public void UpdateCountry(Country country)
        {
            this.Update(country);
        }

        public void UpdateSport(Sport sport)
        {
            this.Update(sport);
        }
    }
}