using IdentitySample.Models;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VacationsBLL.DIModules;

namespace IdentitySample
{
    // Note: For instructions on enabling IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=301868
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule repositoriesDIModule = new RepositoriesDIModules();
            NinjectModule servicesDIModule = new ServicesDIModule();
            var kernel = new StandardKernel(servicesDIModule, repositoriesDIModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }

    }
}
