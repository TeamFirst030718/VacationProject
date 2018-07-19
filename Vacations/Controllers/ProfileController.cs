using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VacationsBLL.Interfaces;

namespace Vacations.Controllers
{
    [Authorize(Roles ="Administrator, Employee, TeamLeader")]
    public class ProfileController : Controller
    {
        private IProfileDataService _profileDataService;

        public ProfileController(IProfileDataService profileDataService)
        {
            _profileDataService = profileDataService;
        }

        // GET: Profile
        public ActionResult Index()
        {
            return View("MyProfile", _profileDataService);
        }

        public ActionResult RequestVocation()
        {
            return View();
        }

    }
}