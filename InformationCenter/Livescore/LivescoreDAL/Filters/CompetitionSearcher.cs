using System.Linq;
using LivescoreDAL.Description;
using LivescoreDAL.Models;

namespace LivescoreDAL.Filters
{
    public class CompetitionSearcher : ISearcher<Competition>
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int? SportId { get; set; }

        public int? CountryId { get; set; }

        public IQueryable<Competition> Search(IQueryable<Competition> source)
        {
            if (this.Id.HasValue)
                return source.Where(c => c.Id == this.Id.Value);

            if (!string.IsNullOrEmpty(this.Name))
                source = source.Where(c => c.Name.Contains(this.Name));

            if (this.SportId.HasValue)
                source = source.Where(c => c.SportId == this.SportId.Value);

            if (this.CountryId.HasValue)
                source = source.Where(c => CountryId == this.CountryId.Value);

            return source;
        }
    }
}