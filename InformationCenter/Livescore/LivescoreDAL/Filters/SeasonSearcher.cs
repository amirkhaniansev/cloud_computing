using System.Linq;
using LivescoreDAL.Description;
using LivescoreDAL.Models;

namespace LivescoreDAL.Filters
{
    public class SeasonSearcher : ISearcher<Season>
    {
        public int? Id { get; set; }

        public int? CompetitionId { get; set; }

        public IQueryable<Season> Search(IQueryable<Season> source)
        {
            if (this.Id.HasValue)
                return source.Where(s => s.Id == this.Id.Value);

            if (this.CompetitionId.HasValue)
                source = source.Where(s => s.CompetitionId == this.CompetitionId.Value);

            return source;
        }
    }
}