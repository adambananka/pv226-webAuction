using System.Data.Entity;
using Castle.Windsor;
using WebAuction.DataAccessLayer.EntityFramework.Tests.Config;

namespace WebAuction.DataAccessLayer.EntityFramework.Tests
{
    public class DatabaseFixture
    {
        internal readonly IWindsorContainer WindsorContainer = new WindsorContainer();

        public DatabaseFixture()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            Database.SetInitializer(new DropCreateDatabaseAlways<WebAuctionDbContext>());
            WindsorContainer.Install(new EntityFrameworkTestInstaller());
        }
    }
}
