using System;
using System.Data.Entity;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WebAuction.Infrastructure;
using WebAuction.Infrastructure.EntityFramework;
using WebAuction.Infrastructure.EntityFramework.UnitOfWork;
using WebAuction.Infrastructure.Query;
using WebAuction.Infrastructure.UnitOfWork;

namespace WebAuction.DataAccessLayer.EntityFramework.Tests.Config
{
    public class EntityFrameworkTestInstaller : IWindsorInstaller
    {
        private const string TestDbConnectionString = "InMemoryTestDBWebAuction";

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Func<DbContext>>()
                    .Instance(InitializeDatabase)
                    .LifestyleTransient(),
                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<EntityFrameworkUnitOfWorkProvider>()
                    .LifestyleSingleton(),
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(EntityFrameworkRepository<>))
                    .LifestyleTransient(),
                Component.For(typeof(IQuery<>))
                    .ImplementedBy(typeof(EntityFrameworkQuery<>))
                    .LifestyleTransient()
            );
        }

        private static DbContext InitializeDatabase()
        {
            var context = new WebAuctionDbContext(Effort.DbConnectionFactory.CreatePersistent(TestDbConnectionString));
            context.Categories.RemoveRange(context.Categories);
            context.SaveChanges();

            return context;
        }
    }
}
