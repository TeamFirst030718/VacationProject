using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vacations.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View("MyProfile");
        }

        public ActionResult RequestVocation()
        {
            return View();
        }

        public ActionResult AddNewEmployee()
        {
            return View();
        }

    }
}