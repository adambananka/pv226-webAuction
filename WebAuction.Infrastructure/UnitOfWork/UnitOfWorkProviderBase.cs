using System;
using System.Threading;

namespace WebAuction.Infrastructure.UnitOfWork
{
    public abstract class UnitOfWorkProviderBase : IUnitOfWorkProvider
    {
        protected readonly AsyncLocal<IUnitOfWork> UoWLocalInstance = new AsyncLocal<IUnitOfWork>();

        public abstract IUnitOfWork Create();

        public IUnitOfWork GetUnitOfWorkInstance()
        {
            if (UoWLocalInstance != null)
            {
                return UoWLocalInstance.Value;
            }

            throw new InvalidOperationException("No Unit Of Work instances");
        }

        public void Dispose()
        {
            UoWLocalInstance.Value?.Dispose();
            UoWLocalInstance.Value = null;
        }
    }
}