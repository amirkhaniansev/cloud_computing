using System.Collections.Generic;
using System.Threading.Tasks;
using LivescoreDAL.Filters;
using LivescoreDAL.Models;

namespace LivescoreDAL.Description
{
    public interface ISportDAL : IBaseDAL
    {
        Task<List<Sport>> GetSports(SportSearcher filter);

        Task<Sport> AddSport(Sport sport);

        void UpdateSport(Sport sport);

        void DeleteSport(Sport sport);

        Task<List<Country>> GetCountries(CountrySearcher filter);

        Task<Country> AddCountry(Country country);

        void UpdateCountry(Country country);

        void DeleteCountry(Country country);

        Task<List<Competition>> GetCompetitions(CompetitionSearcher filter);

        void UpdateCompetition(Competition competition);

        void DeleteCompetition(Competition competition);
    }
}
