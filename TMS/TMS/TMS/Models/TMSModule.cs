using System;
using System.Data;
using System.Collections.Generic;

namespace TMS.Models
{
    public class TMSModule
    {        
        public List<TMSModuleCollection> ModuleList { get; set; }
        public TMSModule()
        {
        }
        public TMSModule(TMSParam model)
        {
            TMSGetData db = new TMSGetData();
            DataTable dt = db.GetQuery(model.QueryName);
            if (db.ErrorMessage == null && dt.Rows.Count > 0)
            {
                string ModuleQuery = dt.Rows[0][1].ToString().Replace("@UserName", model.User);
                string Tablename = dt.Rows[0][2].ToString();
                dt = db.GetDataTable(ModuleQuery, Tablename);
                if (db.ErrorMessage == null && dt.Rows.Count > 0)
                {
                    ModuleList = new List<TMSModuleCollection>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        ModuleList.Add(new TMSModuleCollection(new TMSModuleItem(dr)));
                    }
                }
            }
        }
        public static List<TMSModuleCollection> GetModules(string user)
        {
            TMSGetData db = new TMSGetData();
            List<TMSModuleCollection> ModuleList = new List<TMSModuleCollection>();
            DataTable dt = db.GetQuery("Module");
            if (db.ErrorMessage == null && dt.Rows.Count > 0)
            {
                string ModuleQuery = dt.Rows[0][1].ToString().Replace("@UserName",user);
                string Tablename = dt.Rows[0][2].ToString();
                dt = db.GetDataTable(ModuleQuery, Tablename);
                if (db.ErrorMessage == null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ModuleList.Add(new TMSModuleCollection(new TMSModuleItem(dr)));
                    }
                }
            }
            return ModuleList;
        }
    }
    public class TMSModuleItem
    {
        public string Module_Name { get; set; }
        public string Module_Description { get; set; }
        public string Module_Title { get; set; }
        public TMSModuleItem()
        {
        }
        public TMSModuleItem(DataRow dr)
        {
            int COL_Module_Name = 0;
            int COL_Module_Description = 1;
            int COL_Module_Title = 2;
            if (dr != null)
            {
                if (dr[COL_Module_Name] != DBNull.Value)
                    Module_Name = dr[COL_Module_Name].ToString();
                if (dr[COL_Module_Description] != DBNull.Value)
                    Module_Description = dr[COL_Module_Description].ToString();
                if (dr[COL_Module_Title] != DBNull.Value)
                    Module_Title = dr[COL_Module_Title].ToString();
            }
        }

    }
    public class TMSModuleCollection
    {
        public string Module_Name { get; set; }
        public string Module_Description { get; set; }
        public string Module_Title { get; set; }
        public TMSModuleCollection(TMSModuleItem item)
        {
            Module_Name = item.Module_Name;
            Module_Title = item.Module_Title;
            Module_Description = item.Module_Description;
        }
        public override string ToString()
        {
            return Module_Description;
        }
    }
}