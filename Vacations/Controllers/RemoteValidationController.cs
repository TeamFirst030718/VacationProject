﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VacationsBLL.Interfaces;

namespace Vacations.Controllers
{
    public class RemoteValidationController : Controller
    {
        private IValidateService _validateService;

        public RemoteValidationController(IValidateService validateService)
        {
            _validateService = validateService;
        }

        public JsonResult ValidateEmail(string EmployeeID,string WorkEmail)
        {
            if (EmployeeID=="undefined")
            {
                if (_validateService.CheckEmailForExisting(WorkEmail))
                {
                    return Json(" is in use", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                if (_validateService.CheckEmailForExisting(WorkEmail) && _validateService.CheckEmailOwner(WorkEmail,EmployeeID))
                {
                    return Json(" is in use", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
           
        }
    }
}