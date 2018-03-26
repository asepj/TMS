using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class TMSParam
    {
        public string AreaName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string User { get; set; }
        public int ModuleID { get; set; }
        public int ModuleItem { get; set; }
        public string HomePageID { get; set; }
        public string ChartType { get; set; }
        public int AreaID { get; set; }
        public int EquipmentID { get; set; }
        public string QueryName { get; set; }
    }
}