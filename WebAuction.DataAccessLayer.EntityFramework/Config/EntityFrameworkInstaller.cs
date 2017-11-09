using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WebAuction.Infrastructure.EntityFramework.UnitOfWork;
using WebAuction.Infrastructure.UnitOfWork;

namespace WebAuction.DataAccessLayer.EntityFramework.Config
{
    public class EntityFrameworkInstaller : IWindsorInstaller
    {
        public static string ConnectionSting =
            "Data source=(localdb)\\mssqllocaldb;Database=WebAuctionSample;Trusted_Connection=True;MultipleActiveResultSets=true";

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<EntityFrameworkUnitOfWorkProvider>()
                    .LifestyleSingleton());
        }
    }
}
