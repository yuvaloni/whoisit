using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
namespace whoisit.Controllers
{
    public class ExistsController : ApiController
    {
        public HttpResponseMessage GetExists(string phone)
        {
            
            SqlConnection con = new SqlConnection("Data Source=b471cd39-9c59-46a9-b4d9-a38b00a748aa.sqlserver.sequelizer.com;Initial Catalog=dbb471cd399c5946a9b4d9a38b00a748aa;Persist Security Info=True;User ID=szssaqjrgsgntzbv;Password=YZzoxqA7hHMpQdX5HNyHktmroFS8DjCgEmyWvk6ABTJAahan4t7fFBNEMhaYEBA6");
            con.Open();
            SqlCommand com5 = new SqlCommand("SELECT * FROM users WHERE phone = @i", con);
            com5.Parameters.Add("@i", SqlDbType.NVarChar).Value = phone;
            SqlDataReader r3 = com5.ExecuteReader();
            bool Exists = r3.Read();
            r3.Close();
            var resp = Request.CreateResponse<bool>(HttpStatusCode.OK, Exists);
            con.Close();
            return resp;

        }
    }
}
