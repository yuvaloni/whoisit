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
    public class PostController : ApiController
    {
        public HttpResponseMessage PostPost(string poster, string character, string name, string job)
        {
            SqlConnection con = new SqlConnection("Data Source=b471cd39-9c59-46a9-b4d9-a38b00a748aa.sqlserver.sequelizer.com;Initial Catalog=dbb471cd399c5946a9b4d9a38b00a748aa;Persist Security Info=True;User ID=szssaqjrgsgntzbv;Password=YZzoxqA7hHMpQdX5HNyHktmroFS8DjCgEmyWvk6ABTJAahan4t7fFBNEMhaYEBA6");
            con.Open();
            SqlCommand com5 = new SqlCommand("SELECT * FROM users WHERE id = @i", con);
            com5.Parameters.Add("@i", SqlDbType.NVarChar).Value = poster;
            SqlDataReader r3 = com5.ExecuteReader();
            if (!r3.Read())
            {
                r3.Close();
                con.Close();
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            r3.Close();
            string chars = "1234567890qwertyuiopasdfghjklzxcvbnm";
            string id = "P";
            bool exists;
            Random r = new Random();
            do
            {
                id = "P";
                for (int i = 0; i < 16; i++)
                    id += chars[r.Next(0, chars.Length)];
                SqlCommand com = new SqlCommand("SELECT * FROM "+character+"_CHARACTER WHERE id=@i", con);
                com.Parameters.Add("@i", SqlDbType.NVarChar).Value = id;
                SqlDataReader re = com.ExecuteReader();
                exists = re.Read();
                re.Close();
            } while (exists);


            SqlCommand com2 = new SqlCommand("INSERT INTO "+character+"_CHARACTER(id,[user],likes,reports,name,job,time) VALUES (@i,@u,@l,@r,@n,@j,@t)", con);
            com2.Parameters.Add("@i", SqlDbType.NVarChar).Value = id;
            com2.Parameters.Add("@u", SqlDbType.NVarChar).Value = poster;
            com2.Parameters.Add("@l", SqlDbType.Int).Value = 0;
            com2.Parameters.Add("@r", SqlDbType.Int).Value = 0;
            com2.Parameters.Add("@n", SqlDbType.NVarChar).Value = name;
            com2.Parameters.Add("@j", SqlDbType.NVarChar).Value = job;
            com2.Parameters.Add("@t", SqlDbType.DateTime).Value = DateTime.Now;
            com2.ExecuteNonQuery();

            SqlCommand com4 = new SqlCommand("INSERT INTO "+poster+"_POSTS([character],post) VALUES(@c,@p)", con);
            com4.Parameters.Add("@c", SqlDbType.NVarChar).Value = character;
            com4.Parameters.Add("@p", SqlDbType.NVarChar).Value = id;

            com4.ExecuteNonQuery();
            con.Close();
            var resp = Request.CreateResponse<string>(HttpStatusCode.Created, id);
            return resp;
        }
        public HttpResponseMessage DeletePost(string poster, string character, string id)
        {
            SqlConnection con = new SqlConnection("Data Source=b471cd39-9c59-46a9-b4d9-a38b00a748aa.sqlserver.sequelizer.com;Initial Catalog=dbb471cd399c5946a9b4d9a38b00a748aa;Persist Security Info=True;User ID=szssaqjrgsgntzbv;Password=YZzoxqA7hHMpQdX5HNyHktmroFS8DjCgEmyWvk6ABTJAahan4t7fFBNEMhaYEBA6");
            con.Open();
            SqlCommand com5 = new SqlCommand("SELECT * FROM users WHERE id = @i", con);
            com5.Parameters.Add("@i", SqlDbType.NVarChar).Value = poster;
            SqlDataReader r3 = com5.ExecuteReader();
            if (!r3.Read())
            {
                r3.Close();
                con.Close();
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            r3.Close();
          

            SqlCommand com2 = new SqlCommand("DELETE " + character + "_CHARACTER WHERE id = @i AND user = @u", con);
            com2.Parameters.Add("@i", SqlDbType.NVarChar).Value = id;
            com2.Parameters.Add("@u", SqlDbType.NVarChar).Value = poster;
            com2.ExecuteNonQuery();
            SqlCommand com3 = new SqlCommand("DELETE " + poster + "_POSTS WHERE [character]=@c AND post=@p", con);
            com2.Parameters.Add("@c", SqlDbType.NVarChar).Value = character;
            com2.Parameters.Add("@u", SqlDbType.NVarChar).Value = id;
            com2.ExecuteNonQuery();
            con.Close();
            var resp = Request.CreateResponse<string>(HttpStatusCode.OK, id);
            return resp;
        }
        public HttpResponseMessage GetPost(string user, string character, string id)
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


            SqlCommand com2 = new SqlCommand(" SELECT * from " + character + "_CHARACTER WHERE id = @i", con);
            com2.Parameters.Add("@i", SqlDbType.NVarChar).Value = id;
            SqlDataReader r2 = com2.ExecuteReader();
            string[] post = new string[4];
            if (!r2.Read())
            {
                r2.Close();
                con.Close();
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                string[] shit = { r2.GetString(4), r2.GetString(5), r2.GetDateTime(6).ToString(), r2.GetInt32(2).ToString() };
                post = shit;
            }
            r2.Close();
            con.Close();
            var resp = Request.CreateResponse<string[]>(HttpStatusCode.Created, post);
            return resp;
        }
    }
}
