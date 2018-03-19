
namespace TMS.Models
{
    public class TMSConnection
    {
        public string ConnectionStr { get; set; }
        public TMSConnection()
        {
            ConnectionStr = "Data Source=.;Initial Catalog=TMS;Persist Security Info=True;User ID=sa; Password=1m4dm1n;Connection Timeout=600";
        }

    }
}