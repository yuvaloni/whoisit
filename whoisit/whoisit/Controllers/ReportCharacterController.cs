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
    public class ReportCharacterController : ApiController
    {
        public HttpResponseMessage PutReportCharacter(string reporter, string character)
        {

            SqlConnection con = new SqlConnection("Data Source=b471cd39-9c59-46a9-b4d9-a38b00a748aa.sqlserver.sequelizer.com;Initial Catalog=dbb471cd399c5946a9b4d9a38b00a748aa;Persist Security Info=True;User ID=szssaqjrgsgntzbv;Password=YZzoxqA7hHMpQdX5HNyHktmroFS8DjCgEmyWvk6ABTJAahan4t7fFBNEMhaYEBA6");
            con.Open();
            SqlCommand com5 = new SqlCommand("SELECT * FROM users WHERE id = @i", con);
            com5.Parameters.Add("@i", SqlDbType.NVarChar).Value = reporter;
            SqlDataReader r3 = com5.ExecuteReader();
            if (!r3.Read())
            {
                r3.Close();
                con.Close();
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            r3.Close();
            SqlCommand com3 = new SqlCommand("SELECT * FROM characters WHERE id = @i", con);
            com3.Parameters.Add("@i", SqlDbType.NVarChar).Value = character;
            SqlDataReader r2 = com3.ExecuteReader();
            string id = "";
            int preports = 1;
            if (r2.Read())
            {
                id = r2.GetString(2);
                preports += r2.GetInt32(3);
            }
            r2.Close();
            SqlCommand com4 = new SqlCommand("UPDATE characters SET reports=@t WHERE id=@i", con);
            com4.Parameters.Add("@i", SqlDbType.NVarChar).Value = character;
            com4.Parameters.Add("@t", SqlDbType.Int).Value = preports;
            com4.ExecuteNonQuery();
            SqlCommand com = new SqlCommand("SELECT * FROM users WHERE id = @i", con);
            com.Parameters.Add("@i", SqlDbType.NVarChar).Value = id;

            SqlDataReader r = com.ExecuteReader();
            int reports = 1;
            if (r.Read())
                reports += r.GetInt32(2);
            r.Close();
            SqlCommand com2 = new SqlCommand("UPDATE users SET reports=@t WHERE id=@i ", con);
            com2.Parameters.Add("@i", SqlDbType.NVarChar).Value = id;
            com2.Parameters.Add("@t", SqlDbType.Int).Value = reports;
            com2.ExecuteNonQuery();
            con.Close();
            var resp = Request.CreateResponse<string>(HttpStatusCode.OK, character);
            return resp;
        }
    }
}
