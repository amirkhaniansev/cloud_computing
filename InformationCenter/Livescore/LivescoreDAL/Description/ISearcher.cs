using System.Linq;

namespace LivescoreDAL.Description
{
    public interface ISearcher<TEntity>
    {
        IQueryable<TEntity> Search(IQueryable<TEntity> source);
    }
}
