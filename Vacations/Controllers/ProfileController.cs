using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.Mvc;
using VacationsBLL.Enums;
using Vacations.Models;
using VacationsBLL.DTOs;
using VacationsBLL.Interfaces;
using VacationsBLL.Services;
using PagedList;
using System.Linq;

namespace Vacations.Controllers
{
    [Authorize(Roles = "Administrator, Employee, TeamLeader")]
    public class ProfileController : Controller
    {
        private const int pageSize = 9;
        private readonly IPageListsService _pageListsService;
        private IProfileDataService _profileDataService;
        private IEmployeeService _employeeService;
        private IRequestCreationService _requestCreationService;

        public ProfileController(IProfileDataService profileDataService,
                                 IEmployeeService employeesService,
                                 IPageListsService pageListsService,
                                 IRequestCreationService requestCreationService)
        {
            _profileDataService = profileDataService;
            _employeeService = employeesService;
            _pageListsService = pageListsService;
            _requestCreationService = requestCreationService;
        }

        #region IdentityProps
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }
        #endregion

        [HttpGet]
        [Authorize]
        public ActionResult Index(int page = 1)
        {
            var userData = new UserViewModel
            {
                ProfileData =
                    Mapper.Map<ProfileDTO, ProfileViewModel>(_profileDataService.GetUserData(User.Identity.Name)),
                VacationBalanceData =
                    Mapper.Map<VacationBalanceDTO, VacationBalanceViewModel>(
                        _profileDataService.GetUserVacationBalance(User.Identity.Name)),
                VacationsData =
                    Mapper.MapCollection<ProfileVacationDTO, ProfileVacationsViewModel>(
                        _profileDataService.GetUserVacationsData(User.Identity.Name)).ToPagedList(page,pageSize)
            };
            return View("MyProfile", userData);
        }

        [HttpGet]
        public ActionResult RequestVacation()
        {
            ViewData["VacationTypesSelectList"] = _pageListsService.SelectVacationTypes();
            var requestVacationData = new RequestVacationViewModel
            {
                ProfileData =
                    Mapper.Map<ProfileDTO, ProfileViewModel>(_profileDataService.GetUserData(User.Identity.Name)),
                VacationBalanceData =
                    Mapper.Map<VacationBalanceDTO, VacationBalanceViewModel>(
                        _profileDataService.GetUserVacationBalance(User.Identity.Name)),
                RequestCreationData = new RequestCreationViewModel()
            };
            return View(requestVacationData);
        }

        [HttpPost]
        public ActionResult RequestVacation(RequestCreationViewModel model)
        {
            ViewData["VacationTypesSelectList"] = _pageListsService.SelectVacationTypes();
            var requestVacationData = new RequestVacationViewModel
            {
                ProfileData =
                    Mapper.Map<ProfileDTO, ProfileViewModel>(_profileDataService.GetUserData(User.Identity.Name)),
                VacationBalanceData =
                    Mapper.Map<VacationBalanceDTO, VacationBalanceViewModel>(
                        _profileDataService.GetUserVacationBalance(User.Identity.Name)),
                RequestCreationData = new RequestCreationViewModel()
            };

            if (ModelState.IsValid)
            {
                model.EmployeeID = UserManager.FindByEmail(User.Identity.Name).Id;
                model.VacationID = Guid.NewGuid().ToString();
                model.VacationTypeID = Request.Params["VacationTypesSelectList"];
                model.VacationStatusTypeID = _requestCreationService.GetStatusIdByType(VacationStatusTypeEnum.Pending.ToString());
                model.Created = DateTime.UtcNow;

                _requestCreationService.CreateVacation(Mapper.Map<RequestCreationViewModel, VacationDTO>(model));

                return View(requestVacationData);
            }
            else
            {
                return View(requestVacationData);
            }
        }
        
        [HttpGet]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}