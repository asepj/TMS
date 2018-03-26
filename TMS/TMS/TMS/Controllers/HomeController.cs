using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using TMS.Models;
using DevExpress.Web.Mvc;
using System.Windows.Forms;

namespace TMS.Controllers
{
    public class HomeController : Controller
    {
        string user = (new TMSClientUser()).UserName;
        public ActionResult Index(TMSParam model)
        {
            ViewData["user"] = user;
            TMSGetData db = new TMSGetData();
            model.QueryName = "Title";
            DataTable dt = db.GetQuery(model.QueryName);
            if (db.ErrorMessage == null && dt.Rows.Count > 0)
            {
                model.StartDate = DateTime.Now;
                model.EndDate = DateTime.Now;
                model.User = user;
                model.ModuleID = 1;
                model.EquipmentID = 1;
                model.AreaID = 1;
                ViewData["NewEquipmentID"] = model.EquipmentID;
                string query = dt.Rows[0][1].ToString() + "1";
                dt = db.GetDataTable(query, dt.Rows[0][2].ToString());
                if (db.ErrorMessage == null && dt.Rows.Count > 0)
                    ViewData["Title"] = dt.Rows[0][0].ToString();
            }
            return View(model);
        }
        public ActionResult TMS(TMSParam model)
        {
            ViewData["user"] = user;
            TMSGetData db = new TMSGetData();
            model.QueryName = "Title";
            DataTable dt = db.GetQuery(model.QueryName);
            if (db.ErrorMessage == null && dt.Rows.Count > 0)
            {
                model.StartDate = DateTime.Now.AddDays(-7);
                model.EndDate = DateTime.Now;
                model.User = user;
                model.ModuleID = 2;
                string query = dt.Rows[0][1].ToString() + "2";
                dt = db.GetDataTable(query, dt.Rows[0][2].ToString());
                if (db.ErrorMessage == null && dt.Rows.Count > 0)
                    ViewData["Title"] = dt.Rows[0][0].ToString();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult GetID()
        {
            TMSGetData db = new TMSGetData();
            TMSParam model = new TMSParam();
            model.QueryName = "NextEquipment";
            DataTable dt = db.GetQuery(model.QueryName);
            if (db.ErrorMessage == null && dt.Rows.Count > 0)
            {
                string query = dt.Rows[0][1].ToString().Replace("@EquipmentID", Request.Params["AreaID"].ToString());
                dt = db.GetDataTable(query, dt.Rows[0][2].ToString());
                if (db.ErrorMessage == null && dt.Rows.Count > 0)
                {
                    model.User = user;
                    model.ModuleID = 1;
                    model.EquipmentID = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
            }
            model.StartDate = DateTime.Now;
            return (Json(model.EquipmentID));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult HomePage(TMSParam model)
        {
            if (Request.Params["NewEquipmentID"] != null)
            {
                model.EquipmentID = Convert.ToInt32( Request.Params["NewEquipmentID"].ToString());
                model.StartDate = DateTime.Now;
            }
            return PartialView("HomePage", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TMSPartial(TMSParam model)
        {
            model.QueryName = "ModuleItem";
            List<TMSModuleItemCollection> mic = TMSModuleItems.GetItem(model);
            model.ModuleItem = mic.Find(x => x.Item_Desc == Request.Params["Combobox"].ToString()).Item_ID;
            return PartialView("TMSPartial", model);
        }

        public ActionResult Setting(TMSParam model)
        {
            ViewData["user"] = user;
            TMSGetData db = new TMSGetData();
            model.QueryName = "Title";
            DataTable dt = db.GetQuery(model.QueryName);
            if (db.ErrorMessage == null && dt.Rows.Count > 0)
            {
                string query = dt.Rows[0][1].ToString() + "3";
                dt = db.GetDataTable(query, dt.Rows[0][2].ToString());
                if (db.ErrorMessage == null && dt.Rows.Count > 0)
                    ViewData["Title"] = dt.Rows[0][0].ToString();
            }
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DataView(TMSParam model)
        {
            return PartialView("DataView", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DataViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] TMS.Models.TMSParam item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("DataViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult DataViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] TMS.Models.TMSParam item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("DataViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult DataViewPartialDelete(System.DateTime StartDate)
        {
            var model = new object[0];
            if (StartDate != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("DataViewPartial", model);
        }
    }
}
