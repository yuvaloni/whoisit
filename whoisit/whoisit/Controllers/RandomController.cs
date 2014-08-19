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
    public class RandomController : ApiController
    {
        public HttpResponseMessage GetRandomPost()
        {
            SqlConnection con = new SqlConnection("Data Source=b471cd39-9c59-46a9-b4d9-a38b00a748aa.sqlserver.sequelizer.com;Initial Catalog=dbb471cd399c5946a9b4d9a38b00a748aa;Persist Security Info=True;User ID=szssaqjrgsgntzbv;Password=YZzoxqA7hHMpQdX5HNyHktmroFS8DjCgEmyWvk6ABTJAahan4t7fFBNEMhaYEBA6");
            con.Open();
            SqlCommand com = new SqlCommand("Select * from characters",con);
            SqlDataReader r = com.ExecuteReader();
            List<string> characters = new List<string>();
            while(r.Read())
            {
                characters.Add(r.GetString(0));
            }
            r.Close();
            Random rand = new Random();
            string character = characters[rand.Next(0, characters.Count)];
            SqlCommand com2 = new SqlCommand("Select * from "+character+"_CHARACTER", con);
            List<string> posts = new List<string>();
            SqlDataReader r2 = com2.ExecuteReader();
            while (r2.Read())
                posts.Add(r2.GetString(1));
            r2.Close();
            string post = posts[rand.Next(0, posts.Count)];
            string[] chosen = {character,post};
            con.Close();
            var resp = Request.CreateResponse<string[]>(HttpStatusCode.OK, chosen);
            return resp;

        }
        public HttpResponseMessage GetRandomPostFromFriend(string id)
        {
            SqlConnection con = new SqlConnection("Data Source=b471cd39-9c59-46a9-b4d9-a38b00a748aa.sqlserver.sequelizer.com;Initial Catalog=dbb471cd399c5946a9b4d9a38b00a748aa;Persist Security Info=True;User ID=szssaqjrgsgntzbv;Password=YZzoxqA7hHMpQdX5HNyHktmroFS8DjCgEmyWvk6ABTJAahan4t7fFBNEMhaYEBA6");
            con.Open();
            SqlCommand com = new SqlCommand("Select * from "+id+"_POSTS", con);
            SqlDataReader r = com.ExecuteReader();
            List<string[]> posts = new List<string[]>();
            while (r.Read())
            {
                string[] post = { r.GetString(0), r.GetString(1) };
                posts.Add(post);
            }
            r.Close();
            Random rand = new Random();
            con.Close();
            var resp = Request.CreateResponse<string[]>(HttpStatusCode.OK, posts[rand.Next(0,posts.Count)]);
            return resp;

        }
    }
}
