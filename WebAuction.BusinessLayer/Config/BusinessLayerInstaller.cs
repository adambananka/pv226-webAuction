using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WebAuction.DataAccessLayer.EntityFramework.Config;

namespace WebAuction.BusinessLayer.Config
{
    public class BusinessLayerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            new EntityFrameworkInstaller();
        }
    }
}
