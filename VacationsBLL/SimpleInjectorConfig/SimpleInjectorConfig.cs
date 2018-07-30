using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Web.Mvc;
using VacationsBLL.Interfaces;
using VacationsBLL.Services;
using VacationsDAL.Contexts;
using VacationsDAL.Interfaces;
using VacationsDAL.Repositories;

namespace VacationsBLL.SimpleInjectorConfig
{
    public class SimpleInjectorConfig
    {
        public static void RegisterComponents()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Register<ITeamRepository, TeamRepository>(Lifestyle.Scoped);
            container.Register<ITransactionRepository, TransactionRepository>(Lifestyle.Scoped);
            container.Register<ITransactionTypeRepository, TransactionTypeRepository>(Lifestyle.Scoped);
            container.Register<IVacationRepository, VacationRepository>(Lifestyle.Scoped);
            container.Register<IVacationStatusTypeRepository, VacationStatusTypeRepository>(Lifestyle.Scoped);
            container.Register<IUsersRepository, UsersRepository>(Lifestyle.Scoped);
            container.Register<IRolesRepository, RolesRepository>(Lifestyle.Scoped);
            container.Register<IEmployeeRepository, EmployeeRepository>(Lifestyle.Scoped);
            container.Register<IJobTitleRepository, JobTitleRepository>(Lifestyle.Scoped);
            container.Register<IVacationTypeRepository, VacationTypeRepository>(Lifestyle.Scoped);
            container.Register<VacationsContext>(Lifestyle.Scoped);

            container.Register<IRequestService, RequestService>(Lifestyle.Scoped);
            container.Register<IRoleService, RoleService>(Lifestyle.Scoped);
            container.Register<IUserService, UserService>(Lifestyle.Scoped);
            container.Register<IEmployeeService, EmployeeService>(Lifestyle.Scoped);
            container.Register<IPageListsService, PageListsService>(Lifestyle.Scoped);
            container.Register<IProfileDataService, ProfileDataService>(Lifestyle.Scoped);
            container.Register<ITeamService, TeamService>(Lifestyle.Scoped);
            container.Register<IRequestCreationService, RequestCreationService>(Lifestyle.Scoped);
            container.Register<IAdminEmployeeListService, AdminEmployeeListService>(Lifestyle.Scoped);
            container.Register<IPhotoUploadService, PhotoUploadService>(Lifestyle.Scoped);
            container.Register<IVacationService, VacationService>(Lifestyle.Scoped);

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
