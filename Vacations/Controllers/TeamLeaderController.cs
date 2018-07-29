using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Vacations.Models;
using VacationsBLL.DTOs;
using VacationsBLL.Enums;
using VacationsBLL.Interfaces;
using VacationsBLL.Services;

namespace Vacations.Controllers
{
    [Authorize(Roles = "TeamLeader")]
    public class TeamLeaderController : Controller
    {
        private readonly IPageListsService _pageListsService;
        private readonly IEmployeeService _employeeService;
        private readonly IProfileDataService _profileDataService;
        private readonly IRequestService _requestService;
        private readonly IAdminEmployeeListService _adminEmployeeListService;
        private readonly ITeamService _teamService;
        private readonly IPhotoUploadService _photoUploadService;

        public TeamLeaderController(
            IProfileDataService profileDataService,
            IEmployeeService employeeService,
            IPageListsService pageListsService,
            IAdminEmployeeListService adminEmployeeListService,
            IRequestService requestService,
            ITeamService teamService,
            IPhotoUploadService photoUploadService)
        {
            _profileDataService = profileDataService;
            _employeeService = employeeService;
            _pageListsService = pageListsService;
            _adminEmployeeListService = adminEmployeeListService;
            _requestService = requestService;
            _teamService = teamService;
            _photoUploadService = photoUploadService;
        }


        public ActionResult EmployeesList()
        {
            var employeeList = _adminEmployeeListService.EmployeeList().Where(x=> x.TeamDto.TeamLeadID == User.Identity.GetUserId()).ToList();

            ViewBag.EmployeeService = _employeeService;

            return View(employeeList);
        }

        [HttpGet]
        public ActionResult TeamsList()
        {
            var result = new List<TeamListViewModel>();

            var teams = _teamService.GetAllTeams().Where(x=> x.TeamLeadID == User.Identity.GetUserId());

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
                Status = _employeeService.GetUserById(team.TeamLeadID).Status,
                AmountOfEmployees = team.AmountOfEmployees,
                Employees = employees
            };

            return View(result);
        }

        [HttpGet]
        public ActionResult Requests()
        {
            var requestsData = new VacationRequestsViewModel();

            _requestService.SetReviewerID(User.Identity.GetUserId());

            return View(Mapper.MapCollection<RequestDTO, RequestViewModel>(_requestService.GetRequestsForTeamLeader()));
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

            _requestService.SetReviewerID(User.Identity.GetUserId());

            return View("Requests", Mapper.MapCollection<RequestDTO, RequestViewModel>(_requestService.GetRequestsForTeamLeader()));
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

            return RedirectToAction("Requests", "TeamLeader");
        }
    }
}