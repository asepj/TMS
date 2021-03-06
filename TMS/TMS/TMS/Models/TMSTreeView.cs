﻿using System;
using System.Data;
using System.Collections.Generic;
namespace TMS.Models
{
    public class TMSTreeView
    {
        private TMSTreeNode treeNode;
        private TMSTreeView(TMSParam model)
        {            
            
            TMSGetData db = new TMSGetData();
            DataTable dt = db.GetQuery(model.QueryName);
            if (db.ErrorMessage == null && dt.Rows.Count > 0)
            {
                string query =dt.Rows[0][1].ToString ().Replace("@UserAcc", model.User);
                string TableName = dt.Rows[0][2].ToString();
                dt = db.GetDataTable(query, TableName);
                if (db.ErrorMessage == null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TMSTreeNode tree = new TMSTreeNode();
                        tree.Area_Desc = dr[0].ToString();
                        tree.Area_ID = Convert.ToInt32(dr[1].ToString());
                        if (treeNode == null)
                            treeNode = tree;
                        else
                            treeNode.Nodes.Add(tree);
                    }
                }
            }
        }
        public static TMSTreeNode GetTreeView(TMSParam model)
        {
            TMSTreeView tree = new TMSTreeView(model);
            return tree.treeNode;
        }
    }
}