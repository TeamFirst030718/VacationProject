﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vacations.Models
{
    public class TeamListViewModel
    {
        public string TeamID { get; set; }
        public string TeamName { get; set; }
        public string TeamLeadName { get; set; }
        public int AmountOfEmployees { get; set; }
    }
}