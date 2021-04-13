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
    internal class MatchDAL : BaseDAL, IMatchDAL
    {
        public MatchDAL(DatabaseConfiguration configuration, IBaseDAL parent = null) 
            : base(configuration, parent)
        {
        }

        public Task<Event> AddEvent(Event ev)
        {
            return this.Add(ev);
        }

        public Task<Match> AddMatch(Match match)
        {
            return this.Add(match);
        }

        public void DeleteEvent(Event ev)
        {
            this.Delete(ev);
        }

        public void DeleteMatch(Match match)
        {
            this.Delete(match);
        }

        public Task<List<Event>> GetEvents(EventSearcher filter)
        {
            return this.GetContext()
                       .Events
                       .AsQueryable()
                       .Filter(filter)
                       .ToListAsync();
        }

        public Task<List<Match>> GetMatches(MatchSearcher filter)
        {
            return this.GetContext()
                       .Matches
                       .AsQueryable()
                       .Filter(filter)
                       .ToListAsync();
        }

        public void UpdateEvent(Event ev)
        {
            this.Update(ev);
        }

        public void UpdateMatch(Match match)
        {
            this.Update(match);
        }
    }
}
