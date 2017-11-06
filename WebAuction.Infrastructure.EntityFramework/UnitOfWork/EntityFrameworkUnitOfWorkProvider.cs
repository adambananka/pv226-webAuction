using System;
using System.Data.Entity;
using WebAuction.Infrastructure.UnitOfWork;

namespace WebAuction.Infrastructure.EntityFramework.UnitOfWork
{
    public class EntityFrameworkUnitOfWorkProvider : UnitOfWorkProviderBase
    {
        private readonly Func<DbContext> _dbContextFactory;

        public EntityFrameworkUnitOfWorkProvider(Func<DbContext> dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory;
        }

        public override IUnitOfWork Create()
        {
            UoWLocalInstance.Value = new EntityFrameworkUnitOfWork(_dbContextFactory);
            return UoWLocalInstance.Value;
        }
    }
}
