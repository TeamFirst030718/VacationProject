using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Vacations.Models;
using VacationsBLL.DTOs;
using VacationsBLL.Interfaces;

namespace Vacations.Controllers
{
    [Authorize(Roles ="Administrator, Employee, TeamLeader")]
    public class ProfileController : Controller
    {
        private readonly IPageListsService _pageListsService;

        private IProfileDataService _profileDataService;

        private IEmployeeService _employeeService;

        private IMapService _mapService;

        public ProfileController(IProfileDataService profileDataService, IEmployeeService employees, IPageListsService pageLists, IMapService mapper)
        {
            _profileDataService = profileDataService;
            _employeeService = employees;
            _pageListsService = pageLists;
            _mapService = mapper;
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
            return View(_profileDataService);
        }

        [HttpGet]
        public ActionResult AddNewEmployee()
        {
            return View("Add");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.ListService = _pageListsService;
    
            return View();
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
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Register(EmployeeViewModel model)
        {   
            if (ModelState.IsValid)
            {
                ViewBag.ListService = _pageListsService;

                var jobTitleParam = Request.Params["jobTitlesSelectList"];

                var statusParam = Request.Params["statusSelectList"];
                
                var user = new ApplicationUser { UserName = model.WorkEmail, Email = model.WorkEmail, PhoneNumber = model.PhoneNumber };

                model.JobTitleID = jobTitleParam;

                model.EmployeeID = user.Id;

                model.Status = statusParam.AsBool();

                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var result = await UserManager.CreateAsync(user,"123asdQ!");

                    if (result.Succeeded)
                    {
                        var roleParam = Request.Params["aspNetRolesSelectList"];

                        UserManager.AddToRole(user.Id, roleParam);

                        var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code }, protocol: Request.Url.Scheme);

                        var email = new EmailService();

                        var _employee = _mapService.Map<EmployeeViewModel, EmployeeDTO>(model);

                        _employeeService.Create(_employee);

                        await email.SendAsync(model.WorkEmail, model.Name + " " + model.Surname, "Confirm your account", "Please confirm your account",
                            "Please confirm your account by clicking this <a href=\"" + callbackUrl + "\">link</a>.");

                        ViewBag.Link = callbackUrl;

                        transaction.Complete();

                        return View("Register");
                    }            
                }
            }
           
            return View(model);
        }

    }
}