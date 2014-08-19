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
    public class GetAllFromCharacterController : ApiController
    {
        public HttpResponseMessage GetAllFromCharacter(string user, string character)
        {
            SqlConnection con = new SqlConnection("Data Source=b471cd39-9c59-46a9-b4d9-a38b00a748aa.sqlserver.sequelizer.com;Initial Catalog=dbb471cd399c5946a9b4d9a38b00a748aa;Persist Security Info=True;User ID=szssaqjrgsgntzbv;Password=YZzoxqA7hHMpQdX5HNyHktmroFS8DjCgEmyWvk6ABTJAahan4t7fFBNEMhaYEBA6");
            con.Open();
            SqlCommand com5 = new SqlCommand("SELECT * FROM users WHERE id = @i", con);
            com5.Parameters.Add("@i", SqlDbType.NVarChar).Value = user;
            SqlDataReader r3 = com5.ExecuteReader();
            if (!r3.Read())
            {
                r3.Close();
                con.Close();
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            r3.Close();


            SqlCommand com2 = new SqlCommand(" SELECT * from " + character + "_CHARACTER", con);
            SqlDataReader r2 = com2.ExecuteReader();
            List<string> posts = new List<string>();
            while(r2.Read())
            {
                posts.Add(r2.GetString(0));
                
            }
            r2.Close();
            con.Close();
            string[] post =  new string[posts.Count];
            for(int i =0;i<posts.Count;i++)
            {
                post[0]=posts[0];
            }
            var resp = Request.CreateResponse<string[]>(HttpStatusCode.Created, post);
            return resp;
        }
    }
}
