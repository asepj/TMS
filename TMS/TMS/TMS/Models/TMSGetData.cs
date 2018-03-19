using System;
using System.Data;
using System.Data.SqlClient;

namespace TMS.Models
{
    public class TMSGetData
    {
        public string ErrorMessage { get; set; }
        public TMSGetData()
        {
        }
        public DataTable GetQuery(string QueryName)
        {
            ErrorMessage = null;
            string Query = ("select * from TMS_Get_Data Where QueryName='@QueryName'").Replace("@QueryName",QueryName);
            DataTable dt = new DataTable("QueryValue");
            TMSConnection constr = new TMSConnection();
            SqlConnection conn = new SqlConnection(constr.ConnectionStr);
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(Query, conn);
                command.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
            finally { conn.Close(); }
            return dt;
        }
        public DataTable GetDataTable(string Query, string TableName)
        {
            ErrorMessage = null;
            DataTable dt = new DataTable(TableName);
            TMSConnection constr = new TMSConnection();
            SqlConnection connect = new SqlConnection(constr.ConnectionStr);
            try
            {
                connect.Open();
                SqlCommand command = new SqlCommand(Query, connect);
                command.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);                
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
            finally
            {
                connect.Close();
            }
            return dt;
        }
    }
}