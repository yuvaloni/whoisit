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
    public class UsersController : ApiController
    {
        public HttpResponseMessage PostUser(string phone)
        {
            SqlConnection con = new SqlConnection("Data Source=b471cd39-9c59-46a9-b4d9-a38b00a748aa.sqlserver.sequelizer.com;Initial Catalog=dbb471cd399c5946a9b4d9a38b00a748aa;Persist Security Info=True;User ID=szssaqjrgsgntzbv;Password=YZzoxqA7hHMpQdX5HNyHktmroFS8DjCgEmyWvk6ABTJAahan4t7fFBNEMhaYEBA6");
            string chars = "1234567890qwertyuiopasdfghjklzxcvbnm";
            con.Open();
            string id = "";
            bool exists;
            Random r = new Random();
            do
            {
                id="";
                for(int i =0; i<16;i++)
                    id+=chars[r.Next(0,chars.Length)];
                SqlCommand com = new SqlCommand("SELECT * FROM users WHERE id=@i", con);
                com.Parameters.Add("@i", SqlDbType.NVarChar).Value=id;
                SqlDataReader re = com.ExecuteReader();
                exists=re.Read();
                re.Close();
            }while(exists);


            SqlCommand com2 = new SqlCommand("INSERT INTO users(phone,id,reports,banned) VALUES (@p,@i,@r,@b)",con);
            com2.Parameters.Add("@p", SqlDbType.NVarChar).Value=phone;
            com2.Parameters.Add("@i", SqlDbType.NVarChar).Value=id;
            com2.Parameters.Add("@r", SqlDbType.Int).Value=0;
            com2.Parameters.Add("@b", SqlDbType.Bit).Value=false;
            com2.ExecuteNonQuery();
            SqlCommand com3 = new SqlCommand("CREATE TABLE " + id + "_LIKES([character] nvarchar(max),post nvarchar(max) )", con);
            com3.ExecuteNonQuery();
            SqlCommand com4 = new SqlCommand("CREATE TABLE " + id + "_POSTS([character] nvarchar(max),post nvarchar(max) )",con);
            com4.ExecuteNonQuery();
            con.Close();
            var resp = Request.CreateResponse<string>(HttpStatusCode.Created, id);
            resp.Headers.Location = new Uri(Url.Link("DefaultApi", new { phone = phone }));
            return resp;
        }
        public HttpResponseMessage Getid(string phone)
        {
            SqlConnection con = new SqlConnection("Data Source=b471cd39-9c59-46a9-b4d9-a38b00a748aa.sqlserver.sequelizer.com;Initial Catalog=dbb471cd399c5946a9b4d9a38b00a748aa;Persist Security Info=True;User ID=szssaqjrgsgntzbv;Password=YZzoxqA7hHMpQdX5HNyHktmroFS8DjCgEmyWvk6ABTJAahan4t7fFBNEMhaYEBA6");
            con.Open();
            SqlCommand com = new SqlCommand("SELECT * FROM users  WHERE phone=@p", con);
            com.Parameters.Add("@p", SqlDbType.NVarChar).Value = phone;
            SqlDataReader r = com.ExecuteReader();
            if(r.Read())
            {
                var resp = Request.CreateResponse<string>(HttpStatusCode.Found, r.GetString(1));
                r.Close();
                con.Close();
                return resp;
            }
            else
            {
                r.Close();
                con.Close();
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

        }
        public HttpResponseMessage PutBan(string id)
        {

            SqlConnection con = new SqlConnection("Data Source=b471cd39-9c59-46a9-b4d9-a38b00a748aa.sqlserver.sequelizer.com;Initial Catalog=dbb471cd399c5946a9b4d9a38b00a748aa;Persist Security Info=True;User ID=szssaqjrgsgntzbv;Password=YZzoxqA7hHMpQdX5HNyHktmroFS8DjCgEmyWvk6ABTJAahan4t7fFBNEMhaYEBA6");
            con.Open();
            SqlCommand com = new SqlCommand("UPDATE users SET banned=@t WHERE id=@i", con);
            com.Parameters.Add("@i", SqlDbType.NVarChar).Value = id;
            com.Parameters.Add("@t", SqlDbType.Bit).Value = true;
            com.ExecuteNonQuery();
            con.Close();
            var resp = Request.CreateResponse<string>(HttpStatusCode.Created, id);
            return resp;
        }
        
    }
}
