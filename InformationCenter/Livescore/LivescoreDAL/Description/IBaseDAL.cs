using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using LivescoreDAL.Database;

namespace LivescoreDAL.Description
{
    public interface IBaseDAL : IDisposable, IAsyncDisposable
    {
        Task Save();

        Task<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;

        Task<TEntity> Find<TEntity>(params object[] keys) where TEntity : class;

        void Update<TEntity>(TEntity entity) where TEntity : class;

        void Delete<TEntity>(TEntity entity) where TEntity : class;

        DatabaseFacade GetFacade();

        LivescoreContext GetContext();
    }
}