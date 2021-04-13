using System;
using System.Threading.Tasks;

namespace LivescoreDAL.Base
{
    public abstract class Disposable : IDisposable, IAsyncDisposable
    {
        protected bool disposed;

        public void Dispose()
        {
            this.Dispose(true);
            this.SuppressFinalize();
        }

        public async ValueTask DisposeAsync()
        {
            await this.DisposeAsyncCore();

            this.Dispose(false);
            this.SuppressFinalize();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            this.disposed = true;
        }

        protected virtual ValueTask DisposeAsyncCore()
        {
            return ValueTask.CompletedTask;
        }

        private void SuppressFinalize()
        {
            GC.SuppressFinalize(this);
        }

        ~Disposable()
        {
            this.Dispose(false);
        }
    }
}