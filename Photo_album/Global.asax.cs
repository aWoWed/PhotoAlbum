using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Owin.Security;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Mvc;
using Photo_album.BLL.DI;

namespace Photo_album
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
                
            var module = new BLLModule();
            var kernel = new StandardKernel(module);
            kernel.Bind<IAuthenticationManager>().ToMethod(context => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
