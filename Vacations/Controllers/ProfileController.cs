using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vacations.Models;
using VacationsBLL.Services;


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
        public ActionResult AddNewTeam()
        {
            return View();
        }

        public ActionResult TestView()
        {
            //ViewBag.ListService
            return View();
        }
        

    }
}