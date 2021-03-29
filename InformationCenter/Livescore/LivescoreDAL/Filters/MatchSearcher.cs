using System;
using System.Linq;
using LivescoreDAL.Description;
using LivescoreDAL.Models;

namespace LivescoreDAL.Filters
{
    public class MatchSearcher : ISearcher<Match>
    {
        public long? Id { get; set; }

        public int? HomeTeamId { get; set; }

        public int? AwayTeamId { get; set; }

        public int? SeasonId { get; set; }

        public DateTime? FromStartTime { get; set; }

        public DateTime? ToStartTime { get; set; }

        public IQueryable<Match> Search(IQueryable<Match> source)
        {
            if (this.Id.HasValue)
                return source.Where(m => m.Id == this.Id.Value);

            if (this.HomeTeamId.HasValue)
                source = source.Where(m => m.HomeTeamId == this.HomeTeamId.Value);

            if (this.AwayTeamId.HasValue)
                source = source.Where(m => m.AwayTeamId == this.AwayTeamId.Value);

            if (this.SeasonId.HasValue)
                source = source.Where(m => m.SeasonId == this.SeasonId.Value);

            if (this.FromStartTime.HasValue)
                source = source.Where(m => m.StartTime >= this.FromStartTime.Value);

            if (this.ToStartTime.HasValue)
                source = source.Where(m => m.StartTime <= this.ToStartTime);

            return source;
        }
    }
}
