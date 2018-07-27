using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using VacationsBLL.Enums;
using System.Web.WebPages;
using Vacations.Models;
using VacationsBLL.DTOs;
using VacationsBLL.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Ajax.Utilities;
using VacationsBLL.Services;
using Vacations.Subservice;

namespace Vacations.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IPageListsService _pageListsService;
        private readonly IEmployeeService _employeeService;
        private readonly IProfileDataService _profileDataService;
        private readonly IAdminRequestService _requestService;
        private readonly IAdminEmployeeListService _adminEmployeeListService;
        private readonly ITeamService _teamService;

        public AdminController(
            IProfileDataService profileDataService,
            IEmployeeService employeeService,
            IPageListsService pageListsService,
            IAdminEmployeeListService adminEmployeeListService,
            IAdminRequestService requestService,
            ITeamService TeamService)
        {
            _profileDataService = profileDataService;
            _employeeService = employeeService;
            _pageListsService = pageListsService;
            _adminEmployeeListService = adminEmployeeListService;
            _requestService = requestService;
            _teamService = TeamService;

        }


        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        #region Props
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
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewData["statusSelectList"] = _pageListsService.SelectStatuses();
            ViewData["jobTitlesSelectList"] = _pageListsService.SelectJobTitles();
            ViewData["aspNetRolesSelectList"] = _pageListsService.SelectRoles();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(EmployeeViewModel model)
        {
            ViewData["statusSelectList"] = _pageListsService.SelectStatuses();
            ViewData["jobTitlesSelectList"] = _pageListsService.SelectJobTitles();
            ViewData["aspNetRolesSelectList"] = _pageListsService.SelectRoles();

            if (ModelState.IsValid)
            {
                var role = Request.Params["aspNetRolesSelectList"];

                var user = new ApplicationUser { UserName = model.WorkEmail, Email = model.WorkEmail, PhoneNumber = model.PhoneNumber };

                model.EmployeeID = user.Id;

                model.JobTitleID = Request.Params["jobTitlesSelectList"];

                model.Status = Request.Params["statusSelectList"].AsBool();

                if (await EmployeeCreationService.CreateAndRegisterEmployee(model, role, UserManager, user, _employeeService))
                {
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

                    var email = new EmailService();

                    var codeToSetPassword = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                    var callbackUrlToSetPassword = Url.Action("ResetPasswordAndConfirmEmail", "Account", new { codeToResetPassword = codeToSetPassword, codeToConfirmationEmail = code, userId = user.Id }, protocol: Request.Url.Scheme);

                    await email.SendAsync(model.WorkEmail, model.Name + " " + model.Surname, "Confirm your account", "Please confirm your account",
                        "Set Password and confirm your account by clicking this <a href=\"" + callbackUrlToSetPassword + "\">link</a>.");
                }

                return View("Register");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var role = UserManager.GetRoles(id).FirstOrDefault();

            var model = Mapper.Map<EmployeeDTO, EmployeeViewModel>(_employeeService.GetUserById(id));

            ViewData["statusSelectList"] = _pageListsService.SelectEditStatuses(model.Status.ToString());
            ViewData["jobTitlesSelectList"] = _pageListsService.SelectEditJobTitles(model.JobTitleID);
            ViewData["aspNetRolesSelectList"] = _pageListsService.SelectEditRoles(role);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeViewModel model, string id)
        {
            model.EmployeeID = id;
            var role = UserManager.GetRoles(id).FirstOrDefault();
            ViewData["statusSelectList"] = _pageListsService.SelectEditStatuses(model.Status.ToString());
            ViewData["jobTitlesSelectList"] = _pageListsService.SelectEditJobTitles(model.JobTitleID);
            ViewData["aspNetRolesSelectList"] = _pageListsService.SelectEditRoles(role);

            if (ModelState.IsValid)
            {
                model.JobTitleID = Request.Params["jobTitlesSelectList"];
                model.Status = Request.Params["statusSelectList"].AsBool();
                var roleParam = Request.Params["aspNetRolesSelectList"];

                var userRole = UserManager.GetRoles(model.EmployeeID).First();

                if (userRole != roleParam)
                {
                    UserManager.RemoveFromRole(model.EmployeeID, userRole);
                    UserManager.AddToRoles(model.EmployeeID, roleParam);
                }

                _employeeService.UpdateEmployee(Mapper.Map<EmployeeViewModel, EmployeeDTO>(model));
                return RedirectToAction("Index", "Profile", _profileDataService);
            }

            return View("Edit");
        }

        public ActionResult EmployeesList()
        {
            var employeeList = _adminEmployeeListService.EmployeeList();

            ViewBag.EmployeeService = _employeeService;

            return View(employeeList);
        }

        [HttpGet]
        public ActionResult RegisterTeam()
        {

            ViewData["employeesSelectList"] = _pageListsService.EmployeesList();

            ViewData["listOfEmployees"] = Mapper.MapCollection<EmployeeDTO, EmployeeViewModel>(_employeeService.GetAllFreeEmployees().ToArray());

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterTeam(TeamViewModel model)
        {
            ViewData["employeesSelectList"] = _pageListsService.EmployeesList();

            ViewData["listOfEmployees"] = Mapper.MapCollection<EmployeeDTO, EmployeeViewModel>(_employeeService.GetAllFreeEmployees().ToArray());

            if (ModelState.IsValid)
            {
                model.TeamLeadID = Request.Params["employeesSelectList"];
                model.TeamID = Guid.NewGuid().ToString();

                _teamService.CreateTeam(Mapper.Map<TeamViewModel, TeamDTO>(new TeamViewModel
                {
                    TeamLeadID = model.TeamLeadID,
                    TeamID = model.TeamID,
                    TeamName = model.TeamName
                }));
                string temp = Request.Params["members"];
                if (temp != null)
                {
                    var result = temp.Split(',');
                    foreach (var employeeId in result)
                     {
                        if (employeeId != model.TeamLeadID)
                        {
                            _employeeService.AddToTeam(employeeId, model.TeamID);
                        }
                    }
                }
                ViewBag.ListService = _pageListsService;
                ViewBag.ListOfEmployees = Mapper.MapCollection<EmployeeDTO, EmployeeViewModel>(_employeeService.GetAll().ToArray());
            }

            return View();
        }

        [HttpGet]
        public ActionResult Requests()
        {
            var requestsData = new VacationRequestsViewModel();

            _requestService.SetAdminID(User.Identity.GetUserId());

            return View(Mapper.MapCollection<RequestDTO, RequestViewModel>(_requestService.GetRequests()));
        }

        [HttpGet]
        public ActionResult ProcessPopupPartial(string id)
        {
            var request = Mapper.Map<RequestProcessDTO, RequestProcessViewModel>(_requestService.GetRequestDataById(id));

            return PartialView("ProcessPopupPartial", request);
        }

        [HttpPost]
        public ActionResult ProcessPopupPartial(RequestProcessResultModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Result.Equals(VacationStatusTypeEnum.Approved.ToString()))
                {
                    _requestService.ApproveVacation(Mapper.Map<RequestProcessResultModel, RequestProcessResultDTO>(model));
                }
                else
                {
                    _requestService.DenyVacation(Mapper.Map<RequestProcessResultModel, RequestProcessResultDTO>(model));
                }
            }
            var requestsData = new VacationRequestsViewModel();

            _requestService.SetAdminID(User.Identity.GetUserId());

            return View("Requests", Mapper.MapCollection<RequestDTO, RequestViewModel>(_requestService.GetRequests()));
        }

        [HttpGet]
        public ActionResult TeamsList()
        {
            var result = new List<TeamListViewModel>();

            var teams = _teamService.GetAllTeams();

            foreach (var teamListDto in teams)
            {
                result.Add(new TeamListViewModel
                {
                    TeamID = teamListDto.TeamID,
                    TeamName = teamListDto.TeamName,
                    TeamLeadName = _employeeService.GetUserById(teamListDto.TeamLeadID).Name,
                    AmountOfEmployees = teamListDto.AmountOfEmployees
                });    
            }

            return View(result);
        }

        [HttpGet]
        public ActionResult ViewTeamProfile(string id)
        {
            
            var team = _teamService.GetById(id);

            var employeesDTOs = _employeeService.GetEmployeesByTeamId(team.TeamID);

            var employees = Mapper.MapCollection<EmployeeDTO, EmployeeViewModel>(employeesDTOs);

            var result = new TeamProfileViewModel
            {
                TeamID = team.TeamID,
                TeamName = team.TeamName,
                TeamLeadName = _employeeService.GetUserById(team.TeamLeadID).Name,
                AmountOfEmployees = team.AmountOfEmployees,
                Employees = employees
            };

            return View(result);
        }

        [HttpGet]
        public ActionResult EmployeeView(string id)
        {
            var role = UserManager.GetRoles(id).FirstOrDefault();

            var model = Mapper.Map<EmployeeDTO, EmployeeViewModel>(_employeeService.GetUserById(id));

            ViewData["Status"] = _pageListsService.SelectEditStatuses(model.Status.ToString());
            ViewData["JobTitle"] = _pageListsService.SelectEditJobTitles(model.JobTitleID);
            ViewData["Role"] = _pageListsService.SelectEditRoles(role);

            return View(model);
        }
    }
}
