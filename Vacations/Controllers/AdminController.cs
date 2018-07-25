﻿using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using VacationsBLL.Enums;
using System.Web.WebPages;
using Microsoft.AspNet.Identity.EntityFramework;
using Vacations.Models;
using VacationsBLL.DTOs;
using VacationsBLL.Interfaces;
using VacationsBLL.Services;
using Newtonsoft.Json;

namespace Vacations.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class AdminController : Controller
    {
        private readonly IPageListsService _pageListsService;

        private IEmployeeService _employeeService;

        private IRequestCreationService _requestCreationService;

        private IProfileDataService _profileDataService;

        private IAdminRequestService _requestService;

        private IAdminEmployeeListService _adminEmployeeListService;

        private IMapService _mapService;
        
        private ITeamService _teamService;

        public AdminController(IAspNetUserService AspNetUserService, IProfileDataService AdminDataService, IEmployeeService employees, 
        IPageListsService pageLists, IMapService mapper, ITeamService TeamService,
        IRequestCreationService requestCreationService, IAdminEmployeeListService adminEmployeeListService,
        IAdminRequestService requestService)

        {
            _profileDataService = AdminDataService;
            _employeeService = employees;
            _pageListsService = pageLists;
            _mapService = mapper;
            _requestCreationService = requestCreationService;
            _adminEmployeeListService = adminEmployeeListService;
            _requestService = requestService;
            _teamService = TeamService;
        } 


        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        [AllowAnonymous]
        public ActionResult Register()
        {

            ViewBag.ListService = _pageListsService;

            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(EmployeeViewModel model)
        {
            ViewBag.ListService = _pageListsService;

            if (ModelState.IsValid)
            {
                var jobTitleParam = Request.Params["jobTitlesSelectList"];

                var statusParam = Request.Params["statusSelectList"];

                var user = new ApplicationUser { UserName = model.WorkEmail, Email = model.WorkEmail, PhoneNumber = model.PhoneNumber };

                model.JobTitleID = Request.Params["jobTitlesSelectList"];

                model.EmployeeID = user.Id;

                model.Status = Request.Params["statusSelectList"].AsBool();

                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var result = await UserManager.CreateAsync(user, "123asdQ!");

                    if (result.Succeeded)
                    {
                        var roleParam = Request.Params["aspNetRolesSelectList"];

                        UserManager.AddToRole(user.Id, roleParam);

                        var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

                        var email = new EmailService();

                        var _employee = _mapService.Map<EmployeeViewModel, EmployeeDTO>(model);

                        _employeeService.Create(_employee);

                        var codeToSetPassword = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                        var callbackUrlToSetPassword = Url.Action("ResetPasswordAndConfirmEmail", "Account", new { codeToResetPassword = codeToSetPassword, codeToConfirmationEmail = code, userId = user.Id}, protocol: Request.Url.Scheme);

                        await email.SendAsync(model.WorkEmail, model.Name + " " + model.Surname, "Confirm your account", "Please confirm your account",
                            "Set Password and confirm your account by clicking this <a href=\"" + callbackUrlToSetPassword +"\">link</a>.");

                        transaction.Complete();

                        return View("Register");
                    }
                }
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

            ViewBag.Role = UserManager.GetRoles(id).FirstOrDefault();

            ViewBag.ListService = _pageListsService;

            var model = _mapService.Map<EmployeeDTO, EmployeeViewModel>(_employeeService.GetUserById(id));
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeViewModel model, string id)
        {
            model.EmployeeID = id;
            if (ModelState.IsValid)
            {
                ViewBag.ListService = _pageListsService;
                model.JobTitleID = Request.Params["jobTitlesSelectList"];
                model.Status = Request.Params["statusSelectList"].AsBool();
                var roleParam = Request.Params["aspNetRolesSelectList"];

                var userRole = UserManager.GetRoles(model.EmployeeID).First();

                if (userRole != roleParam)
                {
                    UserManager.RemoveFromRole(model.EmployeeID, userRole);
                    UserManager.AddToRoles(model.EmployeeID, roleParam);
                }
                
                _employeeService.UpdateEmployee(_mapService.Map<EmployeeViewModel, EmployeeDTO>(model));
                return RedirectToAction("Index","Profile", _profileDataService);
            }

            return View("Edit");
        }

        public ActionResult EmployeesList()
        {
            var employeeList = _employeeService.EmployeeList();

            ViewBag.EmployeeService = _employeeService;

            return View(employeeList);
        }

        [HttpGet]
        public ActionResult RegisterTeam()
        {
            ViewBag.ListService = _pageListsService;
            ViewBag.ListOfEmployees = _mapService.MapCollection<EmployeeDTO, EmployeeViewModel>(_employeeService.GetAllFreeEmployees());
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

                _teamService.CreateTeam(_mapService.Map<TeamViewModel, TeamDTO>(new TeamViewModel
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
                ViewBag.ListOfEmployees = _mapService.MapCollection<EmployeeDTO, EmployeeViewModel>(_employeeService.GetAll());
            }
           
            return View();
        }

    


        public ActionResult Requests()
        {
            ViewBag.RequestService = _requestService;
           
            return View();
        }

        [HttpGet]
        public ActionResult ProcessPopupPartial(string id)
        {
            var request = _mapService.Map<RequestProcessDTO, RequestProcessViewModel>(_requestService.GetRequestDataById(id));

            return PartialView("ProcessPopupPartial", request);
        }


        [HttpPost]
        public ActionResult ProcessPopupPartial(RequestProcessResultModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Result.Equals(VacationStatusTypeEnum.Approved.ToString()))
                {                
                    _requestService.ApproveVacation(_mapService.Map<RequestProcessResultModel, RequestProcessResultDTO>(model));
                }
                else
                {
                    _requestService.DenyVacation(_mapService.Map<RequestProcessResultModel, RequestProcessResultDTO>(model));
                }
            }

            ViewBag.RequestService = _requestService;

            return RedirectToAction("Requests", "Admin");

        }
    }
}   
