using System.Linq;
using LivescoreDAL.Description;
using LivescoreDAL.Models;

namespace LivescoreDAL.Filters
{
    public class SeasonTeamSearcher : ISearcher<SeasonTeam>
    {
        public int? SeasonId { get; set; }

        public int? TeamId { get; set; }

        public IQueryable<SeasonTeam> Search(IQueryable<SeasonTeam> source)
        {
            if (this.SeasonId.HasValue)
                source = source.Where(st => st.SeasonId == this.SeasonId.Value);

            if (this.TeamId.HasValue)
                source = source.Where(st => st.TeamId == this.TeamId.Value);

            return source;
        }
    }
}
