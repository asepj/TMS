using System.Data;
using System;
using System.Collections.Generic;
namespace TMS.Models
{
    public class TMSModuleItems
    {
        public List<TMSModuleItemCollection> ItemCollection { get; set; }
        public TMSModuleItems()
        {
        }
        public TMSModuleItems(TMSParam model)
        {
            ItemCollection = new List<TMSModuleItemCollection>();
            TMSGetData db = new TMSGetData();
            DataTable dt = db.GetQuery(model.QueryName);
            if (db.ErrorMessage == null && dt.Rows.Count > 0)
            {
                string query = dt.Rows[0][1].ToString().Replace("@user", model.User).Replace("@moduleid", model.ModuleID.ToString());
                string TableName = dt.Rows[0][2].ToString();
                dt = db.GetDataTable(query, TableName);
                if (db.ErrorMessage == null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                        ItemCollection.Add(new TMSModuleItemCollection(new TMSModuleItemList(dr)));
                }
            }
        }
        public static DataTable GetData(TMSParam model)
        {
            TMSGetData db = new TMSGetData();
            TMSModuleItems moduleitem = new TMSModuleItems(model);
            TMSModuleItemCollection collection = moduleitem.ItemCollection.Find(x => x.Item_ID == model.ModuleItem);
            if (collection.Query == null) return null;
            DataTable dt = db.GetQuery(collection.Query);
            if (db.ErrorMessage == null && dt.Rows.Count > 0)
            {
                string query = dt.Rows[0][1].ToString().Replace("@StartDate", model.StartDate.ToString("yyyy-MM-dd")).Replace("@EndDate", model.EndDate.ToString("yyyy-MM-dd")).Replace("@Area",model.AreaName);
                string TableName = dt.Rows[0][2].ToString();
                dt = db.GetDataTable(query, TableName);
            }
            return dt;
        }
        public static List<TMSModuleItemCollection> GetItem(TMSParam model)
        {
            List<TMSModuleItemCollection> ItemCollection = new List<TMSModuleItemCollection>();
            TMSGetData db = new TMSGetData();
            DataTable dt = db.GetQuery(model.QueryName);
            if (db.ErrorMessage == null && dt.Rows.Count > 0)
            {
                string query = dt.Rows[0][1].ToString().Replace("@user", model.User).Replace("@moduleid", model.ModuleID.ToString());
                string TableName = dt.Rows[0][2].ToString();
                dt = db.GetDataTable(query, TableName);
                if (db.ErrorMessage == null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                        ItemCollection.Add(new TMSModuleItemCollection(new TMSModuleItemList(dr)));
                }
            }
            return ItemCollection;
        }
        public List<TMSModuleItemColumnCollection> GetColumn(TMSParam model)
        {
            List<TMSModuleItemColumnCollection> CollumnCollection = new List<TMSModuleItemColumnCollection>();
            TMSGetData db = new TMSGetData();
            DataTable dt = db.GetQuery(model.QueryName);
            if (db.ErrorMessage == null && dt.Rows.Count > 0)
            {
                string query = dt.Rows[0][1].ToString().Replace("@ItemID", model.ModuleItem.ToString());
                string TableName = dt.Rows[0][2].ToString();
                dt = db.GetDataTable(query, TableName);
                if (db.ErrorMessage == null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                        CollumnCollection.Add(new TMSModuleItemColumnCollection(new TMSModuleItemColumnList(dr)));
                }
            }
            return CollumnCollection;
        }
    }
    public class TMSModuleItemColumnList
    {
        public int Item_ID { get; set; }
        public string Column_Name { get; set; }
        public string Column_Name_View { get; set; }
        public string Column_Datatype { get; set; }
        public TMSModuleItemColumnList()
        {
        }
        public TMSModuleItemColumnList(DataRow dr)
        {
            int COL_Item_ID = 0;
            int COL_Column_Name = 1;
            int COL_Column_Name_View = 2;
            int COL_Column_Datatype = 3;
            if (dr != null)
            {
                if(dr[COL_Item_ID]!= DBNull.Value) Item_ID=Convert.ToInt32(dr[COL_Item_ID].ToString());
                if(dr[COL_Column_Name]!=DBNull.Value) Column_Name=dr[COL_Column_Name].ToString();
                if(dr[COL_Column_Name_View]!=DBNull.Value) Column_Name_View=dr[COL_Column_Name_View].ToString();
                if(dr[COL_Column_Datatype]!=DBNull.Value) Column_Datatype=dr[COL_Column_Datatype].ToString();
            }
        }
    }
    public class TMSModuleItemList
    {
        public string Item_Desc { get; set; }
        public int Item_ID { get; set; }
        public string Query { get; set; }
        public string PageView { get; set; }
        public TMSModuleItemList()
        {
        }
        public TMSModuleItemList(DataRow dr)
        {
            int Col_Item_desc = 0;
            int Col_Item_ID = 1;
            int Col_Query = 2;
            int Col_PageView = 3;
            if (dr != null)
            {
                if (dr[Col_Item_desc] != DBNull.Value)
                    Item_Desc = dr[Col_Item_desc].ToString();
                if (dr[Col_Item_ID] != DBNull.Value)
                    Item_ID = Convert.ToInt32(dr[Col_Item_ID].ToString());
                if (dr[Col_Query] != DBNull.Value) Query = dr[Col_Query].ToString();
                if (dr[Col_PageView] != DBNull.Value) PageView = dr[Col_PageView].ToString();
            }
        }
    }
    public class TMSModuleItemColumnCollection
    {
        public int Item_ID { get; set; }
        public string Column_Name { get; set; }
        public string Column_Name_View { get; set; }
        public string Column_Datatype { get; set; }
        public TMSModuleItemColumnCollection(TMSModuleItemColumnList col)
        {
            Item_ID=col.Item_ID;
            Column_Name=col.Column_Name;
            Column_Name_View=col.Column_Name_View;
            Column_Datatype=col.Column_Datatype;
        }
    }
    public class TMSModuleItemCollection
    {
        public string Item_Desc { get; set; }
        public int Item_ID { get; set; }
        public string Query { get; set; }
        public string PageView { get; set; }
        public TMSModuleItemCollection(TMSModuleItemList item)
        {
            Item_Desc = item.Item_Desc;
            Item_ID = item.Item_ID;
            Query = item.Query;
            PageView = item.PageView;
        }
        public override string ToString()
        {
            return Item_Desc;
        }
    }
}