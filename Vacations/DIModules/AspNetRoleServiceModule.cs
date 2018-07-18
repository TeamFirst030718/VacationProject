using VacationsBLL.Interfaces;
using VacationsBLL.Services;
using Ninject.Modules;

namespace Vacations.DIModules
{
    public class AspNetRoleServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAspNetRoleService>().To<AspNetRoleService>();
        }
    }
}