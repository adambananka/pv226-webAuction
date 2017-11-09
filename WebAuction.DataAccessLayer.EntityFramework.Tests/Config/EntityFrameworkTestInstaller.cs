using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WebAuction.DataAccessLayer.EntityFramework.Entities;
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

            var electro = new Category
            {
                Id = Guid.Parse("00000001-a1de-42ca-ae58-0083fa9f0d7f"),
                Name = "Electronics",
                Description = "Smartphones, laptops and other electronic devices.",
                ParentId = null,
                Parent = null
            };

            var smartphones = new Category
            {
                Id = Guid.Parse("00000011-a1de-42ca-ae58-0083fa9f0d7f"),
                Name = "Smartphones",
                Description = "Smartphone category",
                ParentId = electro.Id,
                Parent = electro
            };

            context.Categories.AddOrUpdate(category => category.Id, electro, smartphones);

            return context;
        }
    }
}
