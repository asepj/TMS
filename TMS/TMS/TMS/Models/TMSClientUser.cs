﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models
{    
    public class TMSClientUser
    {
        public string UserName { get; set; }
        public TMSClientUser()
        {
            UserName = HttpContext.Current.User.Identity.Name.Split('\\').Last();
        }
    }
}