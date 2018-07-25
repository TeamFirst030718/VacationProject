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

namespace Vacations.Controllers
{
    [Authorize(Roles = "Administrator, Employee, TeamLeader")]
    public class ProfileController : Controller
    {
        private readonly IPageListsService _pageListsService;

        private IProfileDataService _profileDataService;

        private IEmployeeService _employeeService;

        private IRequestCreationService _vacationCreationService;

        private IMapService _mapService;

        public ProfileController(IProfileDataService profileDataService, IEmployeeService employees, IPageListsService pageLists,IRequestCreationService vacationService, IMapService mapper)
        {
            _profileDataService = profileDataService;
            _employeeService = employees;
            _pageListsService = pageLists;
            _mapService = mapper;
            _vacationCreationService = vacationService;
        }

        public ProfileController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
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

        [HttpGet]
        public ActionResult Index()
        {
            return View("MyProfile", _profileDataService);
        }

        [HttpGet]
        public ActionResult RequestVacation()
        {
            ViewBag.PageListsService = _pageListsService;

            return View(_profileDataService);
        }

        [HttpPost]
        public ActionResult RequestVacation(RequestCreationModel model)
            {
            model.EmployeeID = UserManager.FindByEmail(User.Identity.Name).Id;
            model.VacationID = Guid.NewGuid().ToString();
            model.VacationTypeID = Request.Params["VacationTypesSelectList"];
            model.VacationStatusTypeID = _vacationCreationService.GetStatusIdByType(VacationStatusTypeEnum.Pending.ToString());
            model.Created = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                _vacationCreationService.CreateVacation(_mapService.Map<RequestCreationModel, VacationDTO>(model));

                ViewBag.PageListsService = _pageListsService;

                return View(_profileDataService);
            }
            else
            {
                ViewBag.PageListsService = _pageListsService;

                return View(_profileDataService);
            }         
        }     

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        
        public ActionResult _ShowUserProfilePartial()
        {
            var userData = _profileDataService.MapEntity<UserProfileDTO, ProfileViewModel>(_profileDataService.GetUserData(User.Identity.Name));
            return PartialView(userData);
        }

    }
}