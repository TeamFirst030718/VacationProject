using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VacationsBLL.Enums;
using System.Web.WebPages;
using Vacations.Models;
using VacationsBLL.DTOs;
using VacationsBLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Transactions;
using VacationsBLL.Services;
using Vacations.Subservice;
using PagedList;
using Vacations.Subservices;

namespace Vacations.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private const int requestPageSize = 15;
        private const int teamPageSize = 15;
        private const int employeePageSize = 10;
        private readonly IPageListsService _pageListsService;
        private readonly IEmployeeService _employeeService;
        private readonly IProfileDataService _profileDataService;
        private readonly IRequestService _requestService;
        private readonly IAdminEmployeeListService _adminEmployeeListService;
        private readonly ITeamService _teamService;
        private readonly IPhotoUploadService _photoUploadService;
        private IValidateService _validateService;
        public AdminController(
            IProfileDataService profileDataService,
            IEmployeeService employeeService,
            IPageListsService pageListsService,
            IAdminEmployeeListService adminEmployeeListService,
            IRequestService requestService,
            ITeamService teamService,
            IPhotoUploadService photoUploadService,
            IValidateService validateService)
        {
            _profileDataService = profileDataService;
            _employeeService = employeeService;
            _pageListsService = pageListsService;
            _adminEmployeeListService = adminEmployeeListService;
            _requestService = requestService;
            _teamService = teamService;
            _photoUploadService = photoUploadService;
            _validateService = validateService;
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
        public async Task<ActionResult> Register(EmployeeViewModel model, HttpPostedFileBase photo)
        {

            ViewData["statusSelectList"] = _pageListsService.SelectStatuses();
            ViewData["jobTitlesSelectList"] = _pageListsService.SelectJobTitles();
            ViewData["aspNetRolesSelectList"] = _pageListsService.SelectRoles();

            if (ModelState.IsValid)
            {
                var role = Request.Params["aspNetRolesSelectList"];

                var user = new ApplicationUser
                {
                    UserName = model.WorkEmail,
                    Email = model.WorkEmail,
                    PhoneNumber = model.PhoneNumber
                };

                model.EmployeeID = user.Id;

                model.JobTitleID = Request.Params["jobTitlesSelectList"];

                model.Status = Request.Params["statusSelectList"].AsBool();

                if (EmployeeCreationService.CreateAndRegisterEmployee(model, role, UserManager, user,
                    _employeeService))
                {

                    _photoUploadService.UploadPhoto(photo, model.EmployeeID);

                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

                    var email = new EmailService();

                    var codeToSetPassword = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                    var callbackUrlToSetPassword = Url.Action("ResetPasswordAndConfirmEmail", "Account",
                        new
                        {
                            codeToResetPassword = codeToSetPassword,
                            codeToConfirmationEmail = code,
                            userId = user.Id
                        }, protocol: Request.Url.Scheme);

                    await email.SendAsync(model.WorkEmail, model.Name + " " + model.Surname, "Confirm your account",
                        "Please, confirm your account",
                        "Set Password and confirm your account by clicking this <a href=\"" +
                        callbackUrlToSetPassword + "\">link</a>.");
                }

                return RedirectToAction("Index", "Profile");
            }

            return RedirectToAction("EmployeesList","Admin");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return View("ErrorPage");
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
        public ActionResult Edit(EmployeeViewModel model, string id, HttpPostedFileBase photo)
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

                var user = UserManager.FindById(User.Identity.GetUserId());

                EmployeeEditService.EditEmployee(model, roleParam, UserManager, user, _employeeService, id);

                if (photo != null)
                {
                    _photoUploadService.UploadPhoto(photo, model.EmployeeID);
                }

                return RedirectToAction("Index", "Profile", _profileDataService);
            }

            return View("Edit");
        }

        public ActionResult EmployeesList(int page = 1)
        {
            var employeeList = _adminEmployeeListService.EmployeeList();

            ViewBag.EmployeeService = _employeeService;
            ViewBag.TeamService = _teamService;

            return View(employeeList.OrderBy(x=>x.TeamDto.TeamName).ToPagedList(page, employeePageSize));
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
            if (ModelState.IsValid)
            {
                model.TeamLeadID = Request.Params["employeesSelectList"];
                model.TeamID = Guid.NewGuid().ToString();

                TeamCreationService.RegisterTeam(model, Request.Params["members"], _employeeService, _teamService,  UserManager);


                ViewBag.ListService = _pageListsService;
                ViewBag.ListOfEmployees =
                    Mapper.MapCollection<EmployeeDTO, EmployeeViewModel>(_employeeService.GetAll().ToArray());

                ViewData["employeesSelectList"] = _pageListsService.EmployeesList();

                ViewData["listOfEmployees"] =
                    Mapper.MapCollection<EmployeeDTO, EmployeeViewModel>(_employeeService.GetAllFreeEmployees()
                        .ToArray());
            }

            return RedirectToAction("RegisterTeam", "Admin");
        }

        [HttpGet]
        public ActionResult Requests(int page = 1)
        {
            var requestsData = new VacationRequestsViewModel();
            _requestService.SetReviewerID(User.Identity.GetUserId());
            var map = Mapper.MapCollection<RequestDTO, RequestViewModel>(_requestService.GetRequestsForAdmin());
            var list = map.ToPagedList(page, requestPageSize);
            return View(list);
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
            _requestService.SetReviewerID(User.Identity.GetUserId());

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

            var map = Mapper.MapCollection<RequestDTO, RequestViewModel>(_requestService.GetRequestsForAdmin());

            var list = map.ToPagedList(1, requestPageSize);

            return View("Requests", list);
        }

        [HttpGet]
        public ActionResult TeamsList(int page = 1)
        {
            var result = new List<TeamListViewModel>();

            var teams = _teamService.GetAllTeams();

            foreach (var teamListDto in teams)
            {
                var teamLead = _employeeService.GetUserById(teamListDto.TeamLeadID);

                result.Add(new TeamListViewModel
                {
                    TeamID = teamListDto.TeamID,
                    TeamName = teamListDto.TeamName,
                    TeamLeadName = teamLead.Name + " " + teamLead.Surname,
                    AmountOfEmployees = teamListDto.AmountOfEmployees
                });
            }

            return View(result.ToPagedList(page, teamPageSize));
        }

        [HttpGet]
        public ActionResult ViewTeamProfile(string id)
        {

            var team = _teamService.GetById(id);

            var employeesDTOs = _employeeService.GetEmployeesByTeamId(team.TeamID);

            var teamLead = _employeeService.GetUserById(team.TeamLeadID);

            var employees = Mapper.MapCollection<EmployeeDTO, EmployeeViewModel>(employeesDTOs.ToArray());

            var result = new TeamProfileViewModel
            {
                TeamID = team.TeamID,
                TeamName = team.TeamName,
                TeamLeadName = teamLead.Name + " " + teamLead.Surname,
                Status = teamLead.Status,
                AmountOfEmployees = team.AmountOfEmployees,
                Employees = employees
            };

            return View(result);
        }

        [HttpGet]
        public ActionResult EmployeeView(string id)
        {
            var employee = _employeeService.GetUserById(id);

            if (employee != null)
            {
                var model = Mapper.Map<EmployeeDTO, EmployeeViewModel>(employee);

                ViewData["Status"] = _employeeService.GetStatusByEmployeeId(model.EmployeeID);
                ViewData["JobTitle"] = _employeeService.GetJobTitleById(model.JobTitleID).JobTitleName;
                ViewData["Role"] = _employeeService.GetRoleByUserId(model.EmployeeID);

                return View(model);
            }

            return RedirectToAction("Requests", "Admin");
        }

        [HttpGet]
        public ActionResult EditTeam(string id)
        {
            var team = Mapper.Map<TeamDTO, TeamViewModel>(_teamService.GetTeamById(id));

            var employees =
                Mapper.MapCollection<EmployeeDTO, EmployeeViewModel>(_employeeService.GetEmployeesByTeamId(id).ToArray());

            ViewData["employeesSelectList"] = _pageListsService.EmployeesList(team.TeamLeadID);
            ViewData["listOfEmployees"] = Mapper.MapCollection<EmployeeDTO, EmployeeViewModel>(_employeeService.GetAllFreeEmployees().ToArray());
            ViewData["Employees"] = employees;
            ViewData["Team"] = team;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTeam(TeamViewModel model, string id)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                model.TeamLeadID = Request.Params["employeesSelectList"];
                model.TeamID = id;
                string members = Request.Params["members"];

                var employees = Mapper.MapCollection<EmployeeDTO, EmployeeViewModel>(_employeeService.GetEmployeesByTeamId(id).ToArray());

                _teamService.UpdateTeamInfo(Mapper.Map<TeamViewModel, TeamDTO>(model));

                var userRole = UserManager.GetRoles(model.TeamLeadID).First();

                if (userRole.Equals(RoleEnum.Employee.ToString()))
                {
                    UserManager.RemoveFromRole(model.TeamLeadID, userRole);
                    UserManager.AddToRoles(model.TeamLeadID, RoleEnum.TeamLeader.ToString());
                }

                var oldEmployeesID = new List<string>();

                foreach (var employee in employees)
                {
                    oldEmployeesID.Add(employee.EmployeeID);
                }

                if (members == null)
                {
                    foreach (var employeeId in oldEmployeesID)
                    {
                        if (employeeId != model.TeamLeadID)
                        {
                            _employeeService.RemoveFromTeam(employeeId, model.TeamID);
                        }
                    }

                    _teamService.DeleteTeam(model.TeamID);

                    transaction.Complete();

                    return RedirectToAction("TeamsList", "Admin", _profileDataService);
                }

                var newEmployeesID = members.Split(',').ToList();

                var employeesToRemove = oldEmployeesID.Except(newEmployeesID);

                var employeesToAdd = newEmployeesID.Except(oldEmployeesID);

                foreach (var employeeId in employeesToAdd)
                {
                    if (employeeId != model.TeamLeadID)
                    {
                        _employeeService.AddToTeam(employeeId, model.TeamID);
                    }
                }

                foreach (var employeeId in employeesToRemove)
                {
                    if (employeeId != model.TeamLeadID)
                    {
                        _employeeService.RemoveFromTeam(employeeId, model.TeamID);
                    }
                }

                transaction.Complete();
            }

            return RedirectToAction("TeamsList", "Admin");
        }
    }
}
