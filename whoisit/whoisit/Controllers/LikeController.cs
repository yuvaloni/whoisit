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
    public class LikeController : ApiController
    {
        public HttpResponseMessage PutLike(string liker, string character, string post)
        {

            SqlConnection con = new SqlConnection("Data Source=b471cd39-9c59-46a9-b4d9-a38b00a748aa.sqlserver.sequelizer.com;Initial Catalog=dbb471cd399c5946a9b4d9a38b00a748aa;Persist Security Info=True;User ID=szssaqjrgsgntzbv;Password=YZzoxqA7hHMpQdX5HNyHktmroFS8DjCgEmyWvk6ABTJAahan4t7fFBNEMhaYEBA6");
            con.Open();
            SqlCommand com5 = new SqlCommand("SELECT * FROM users WHERE id = @i", con);
            com5.Parameters.Add("@i", SqlDbType.NVarChar).Value = liker;
            SqlDataReader r3 = com5.ExecuteReader();
            if (!r3.Read())
            {
                r3.Close();
                con.Close();
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            r3.Close();
            SqlCommand com3 = new SqlCommand("SELECT * FROM " + character + "_CHARACTER WHERE id = @i", con);
            com3.Parameters.Add("@i", SqlDbType.NVarChar).Value = post;
            SqlDataReader r2 = com3.ExecuteReader();
            int plikes = 1;
            if (r2.Read())
            {
                plikes += r2.GetInt32(2);
            }
            r2.Close();
            SqlCommand com4 = new SqlCommand("UPDATE " + character + "_CHARACTER SET likes=@t WHERE id=@i", con);
            com4.Parameters.Add("@i", SqlDbType.NVarChar).Value = post;
            com4.Parameters.Add("@t", SqlDbType.Int).Value = plikes;
            com4.ExecuteNonQuery();
            SqlCommand com = new SqlCommand("INSERT INTO " + liker + "_LIKES([character],post) VALUES(@c,@p)", con);
            com4.Parameters.Add("@c", SqlDbType.NVarChar).Value = character;
            com4.Parameters.Add("@p", SqlDbType.NVarChar).Value = post;
            con.Close();
            var resp = Request.CreateResponse<string>(HttpStatusCode.OK, post);
            return resp;
        }
    }
}
