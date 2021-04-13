using System.Linq;
using LivescoreDAL.Description;
using LivescoreDAL.Models;

namespace LivescoreDAL.Filters
{
    public class SportSearcher : ISearcher<Sport>
    {
        public int? Id { get; set; }

        public int? OriginCountryId { get; set; }

        public string Name { get; set; }

        public IQueryable<Sport> Search(IQueryable<Sport> source)
        {
            if (this.Id.HasValue)
                return source.Where(s => s.Id == this.Id.Value);

            if (this.OriginCountryId.HasValue)
                source = source.Where(s => s.OriginCountryId == this.OriginCountryId.Value);

            if (!string.IsNullOrEmpty(this.Name))
                source = source.Where(s => s.Name.Contains(this.Name));

            return source;
        }
    }
}