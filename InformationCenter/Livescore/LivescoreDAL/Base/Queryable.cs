using System;
using System.Linq;
using LivescoreDAL.Description;

namespace LivescoreDAL.Base
{
    public static class Queryable
    {
        public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> source, ISearcher<TEntity> filter)
        {
            if (source == null)
                throw new NullReferenceException();

            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            return filter.Search(source);
        }
    }
}