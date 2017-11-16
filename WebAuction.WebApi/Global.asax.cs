using System.Web.Http;
using System.Web.Http.Dispatcher;
using Castle.Windsor;
using WebAuction.BusinessLayer.Config;
using WebAuction.WebApi.Filters;
using WebAuction.WebApi.Windsor;

namespace WebAuction.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private readonly IWindsorContainer _container = new WindsorContainer();

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new ErrorHandlingFilter());

            BootstrapContainer();
        }

        private void BootstrapContainer()
        {
            _container.Install(new WebApiInstaller());
            _container.Install(new BusinessLayerInstaller());

            GlobalConfiguration.Configuration.Services
                .Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(_container));
        }

        public override void Dispose()
        {
            _container.Dispose();
            base.Dispose();
        }

    }
}
