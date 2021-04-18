using System;
using System.Linq;
using LivescoreDAL.Description;
using LivescoreDAL.Models;

namespace LivescoreDAL.Filters
{
    public class EventSearcher : ISearcher<Event>
    {
        public long? Id { get; set; }

        public int? MatchId { get; set; }

        public int? TeamId { get; set; }

        public short? Type { get; set; }

        public DateTime? FromTime { get; set; }

        public DateTime? ToTime { get; set; }

        public IQueryable<Event> Search(IQueryable<Event> source)
        {
            if (this.Id.HasValue)
                return source.Where(e => e.Id == this.Id.Value);

            if (this.MatchId.HasValue)
                source = source.Where(e => e.MatchId == this.MatchId.Value);

            if (this.TeamId.HasValue)
                source = source.Where(e => e.TeamId == this.TeamId.Value);

            if (this.Type.HasValue)
                source = source.Where(e => e.Type == this.Type.Value);

            if (this.FromTime.HasValue)
                source = source.Where(e => e.Time >= this.FromTime.Value);

            if (this.ToTime.HasValue)
                source = source.Where(e => e.Time <= this.ToTime.Value);

            return source;
        }
    }
}