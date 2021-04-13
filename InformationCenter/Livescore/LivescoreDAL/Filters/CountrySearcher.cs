using System.Linq;
using LivescoreDAL.Description;
using LivescoreDAL.Models;

namespace LivescoreDAL.Filters
{
    public class CountrySearcher : ISearcher<Country>
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public IQueryable<Country> Search(IQueryable<Country> source)
        {
            if (this.Id.HasValue)
                return source.Where(c => c.Id == this.Id.Value);

            if (!string.IsNullOrEmpty(this.Name))
                source = source.Where(c => c.Name.Contains(this.Name));

            return source;
        }
    }
}