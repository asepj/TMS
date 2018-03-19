using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class TMSParam
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string User { get; set; }
        public int ModuleID { get; set; }
        public string AreaName { get; set; }
        public int ModuleItem { get; set; }
    }
}