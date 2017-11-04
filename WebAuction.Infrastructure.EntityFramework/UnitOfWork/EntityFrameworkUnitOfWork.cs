using System;
using System.Data.Entity;
using System.Threading.Tasks;
using WebAuction.Infrastructure.UnitOfWork;

namespace WebAuction.Infrastructure.EntityFramework.UnitOfWork
{
    public class EntityFrameworkUnitOfWork : UnitOfWorkBase
    {
        /// <summary>
        /// Gets the <see cref="DbContext"/>.
        /// </summary>
        public DbContext Context { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrameworkUnitOfWork"/> class.
        /// </summary>
        public EntityFrameworkUnitOfWork(Func<DbContext> dbContextFactory)
        {
            if (dbContextFactory == null)
            {
                throw new ArgumentException("Db context factory cant be null!");
            }

            Context = dbContextFactory();
        }

        /// <summary>
        /// Commits the changes.
        /// </summary>
        protected override async Task CommitCore()
        {
            await Context.SaveChangesAsync();
        }

        public override void Dispose()
        {
            Context.Dispose();
        }
    }
}