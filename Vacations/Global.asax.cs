using IdentitySample.Models;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Vacations.DImodules;
using Vacations.DIModules;

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

            NinjectModule pageModule = new PageListsServiceModule();
            NinjectModule employeeModule = new EmployeeServiceModule();
            NinjectModule aspNetRoleModule = new AspNetRoleServiceModule();
            NinjectModule unitOfWorkModule = new UnitOfWorkModule();
            var kernel = new StandardKernel(employeeModule, unitOfWorkModule, aspNetRoleModule,pageModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
