using System.Linq;
using LivescoreDAL.Description;
using LivescoreDAL.Models;

namespace LivescoreDAL.Filters
{
    public class TeamSearcher : ISearcher<Team>
    {
        public int? Id { get; set; }

        public int? CountryId { get; set; }

        public int? SportId { get; set; }

        public string Name { get; set; }

        public string Stadium { get; set; }

        public IQueryable<Team> Search(IQueryable<Team> source)
        {
            if (this.Id.HasValue)
                return source.Where(t => t.Id == this.Id.Value);

            if (this.CountryId.HasValue)
                source = source.Where(t => t.CountryId == this.CountryId.Value);

            if (this.SportId.HasValue)
                source = source.Where(t => t.SportId == this.SportId.Value);

            if (!string.IsNullOrEmpty(this.Name))
                source = source.Where(t => t.Name.Contains(this.Name));

            if (!string.IsNullOrEmpty(this.Stadium))
                source = source.Where(t => t.Stadium.Contains(this.Stadium));

            return source;
        }
    }
}
