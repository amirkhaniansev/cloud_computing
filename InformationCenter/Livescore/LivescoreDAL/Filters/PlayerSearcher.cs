using System;
using System.Linq;
using LivescoreDAL.Description;
using LivescoreDAL.Models;

namespace LivescoreDAL.Filters
{
    public class PlayerSearcher : ISearcher<Player>
    {
        public int? Id { get; set; }

        public int? TeamId { get; set; }

        public string Name { get; set; }

        public DateTime? FromBorn { get; set; }

        public DateTime? ToBorn { get; set; }

        public IQueryable<Player> Search(IQueryable<Player> source)
        {
            if (this.Id.HasValue)
                return source.Where(p => p.Id == this.Id.Value);

            if (this.TeamId.HasValue)
                source = source.Where(p => p.TeamId == this.TeamId.Value);

            if (!string.IsNullOrEmpty(this.Name))
                source = source.Where(p => p.Name.Contains(this.Name));

            if (this.FromBorn.HasValue)
                source = source.Where(p => p.Born >= this.FromBorn.Value);

            if (this.ToBorn.HasValue)
                source = source.Where(p => p.Born <= this.ToBorn.Value);

            return source;
        }
    }
}