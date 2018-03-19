using DevExpress.Web.Mvc;

namespace TMS.Models
{
    public class TMSTreeNode: MVCxTreeViewNode
    {
        public TMSTreeNode()
        {
            Image.Url = "~/Images/Icons/display-type.png";
        }
        private string _area_desc;
        public string Area_Desc
        {
            get { return _area_desc; }
            set { _area_desc = value; Name = value; Text = value; }
        }
        private int _area_id;
        public int Area_ID
        {
            get { return _area_id; }
            set { _area_id = value; }
        }
        public override string ToString()
        {
            return Area_Desc;
        }
    }
}