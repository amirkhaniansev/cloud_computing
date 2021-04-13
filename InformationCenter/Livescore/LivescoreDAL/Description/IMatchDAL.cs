using LivescoreDAL.Filters;
using LivescoreDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivescoreDAL.Description
{
    public interface IMatchDAL : IBaseDAL
    {
        Task<Match> AddMatch(Match match);

        Task<List<Match>> GetMatches(MatchSearcher filter);

        void UpdateMatch(Match match);

        void DeleteMatch(Match match);
        
        Task<Event> AddEvent(Event ev);

        Task<List<Event>> GetEvents(EventSearcher filter);

        void UpdateEvent(Event ev);

        void DeleteEvent(Event ev);
    }
}