using System;
using System.Data.Entity;
using WebAuction.Infrastructure.UnitOfWork;

namespace WebAuction.Infrastructure.EntityFramework.UnitOfWork
{
    public class EntityFrameworkUnitOfWorkProvider : UnitOfWorkProviderBase
    {
        private readonly Func<DbContext> dbContextFactory;

        public EntityFrameworkUnitOfWorkProvider(Func<DbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public override IUnitOfWork Create()
        {
            UoWLocalInstance.Value = new EntityFrameworkUnitOfWork(dbContextFactory);
            return UoWLocalInstance.Value;
        }
    }
}
