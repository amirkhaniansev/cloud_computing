using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using LivescoreDAL.Base;
using LivescoreDAL.Description;
using LivescoreDAL.Parameters;

namespace LivescoreDAL.Database
{
    internal abstract class BaseDAL : Disposable, IBaseDAL
    {
        private readonly bool isTest;
        private readonly string databaseName;
        private readonly string connectionString;
        private readonly IBaseDAL parent;
        private readonly LivescoreContext dbContext;

        public BaseDAL(DatabaseConfiguration configuration, IBaseDAL parent = null)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (string.IsNullOrEmpty(configuration.ConnectionString))
                throw new ArgumentException(nameof(configuration.ConnectionString));

            this.isTest = configuration.IsTest;
            this.databaseName = configuration.DatabaseName;
            this.connectionString = configuration.ConnectionString;
            this.parent = parent;
            this.dbContext = parent != null ? parent.GetContext() : new LivescoreContext(configuration);
        }

        public async Task<TEntity> Add<TEntity>(TEntity entity) where TEntity : class
        {
            var entry = await this.dbContext.AddAsync(entity);
            if (entry != null)
                return entry.Entity;

            return null;
        }

        public async Task<TEntity> Find<TEntity>(params object[] keys) where TEntity : class
        {
            return await this.dbContext.FindAsync<TEntity>(keys);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            this.dbContext.Update(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            _ = this.dbContext.Remove(entity);
        }

        public async Task Save()
        {
            if (!await this.dbContext.Save())
                throw new InvalidOperationException();
        }

        public DatabaseFacade GetFacade()
        {
            return this.dbContext.Database;
        }

        public LivescoreContext GetContext()
        {
            return this.dbContext;
        }

        protected override void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            if (disposing && this.parent == null)
                this.dbContext.Dispose();

            base.Dispose(disposing);
        }

        protected override async ValueTask DisposeAsyncCore()
        {
            await this.dbContext.DisposeAsync();
            await base.DisposeAsync();
        }
    }
}