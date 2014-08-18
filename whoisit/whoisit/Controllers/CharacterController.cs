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
    public class CharacterController : ApiController
    {
        public HttpResponseMessage PostCharacter(string link, string creator)
        {
            SqlConnection con = new SqlConnection("Data Source=b471cd39-9c59-46a9-b4d9-a38b00a748aa.sqlserver.sequelizer.com;Initial Catalog=dbb471cd399c5946a9b4d9a38b00a748aa;Persist Security Info=True;User ID=szssaqjrgsgntzbv;Password=YZzoxqA7hHMpQdX5HNyHktmroFS8DjCgEmyWvk6ABTJAahan4t7fFBNEMhaYEBA6");
            string chars = "1234567890qwertyuiopasdfghjklzxcvbnm";
            con.Open();
            SqlCommand com5 = new SqlCommand("SELECT * FROM users WHERE id = @i", con);
            com5.Parameters.Add("@i", SqlDbType.NVarChar).Value = creator;
            SqlDataReader r3 = com5.ExecuteReader();
            if (!r3.Read())
            {
                r3.Close();
                con.Close();
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            r3.Close();
            string id = "C";
            bool exists;
            Random r = new Random();
            do
            {
                id = "C";
                for (int i = 0; i < 16; i++)
                    id += chars[r.Next(0, chars.Length)];
                SqlCommand com = new SqlCommand("SELECT * FROM characters WHERE id=@i", con);
                com.Parameters.Add("@i", SqlDbType.NVarChar).Value = id;
                SqlDataReader re = com.ExecuteReader();
                exists = re.Read();
                re.Close();
            } while (exists);


            SqlCommand com2 = new SqlCommand("INSERT INTO characters(id,link, [user], reports) VALUES (@i,@l, @u, @r)", con);
            com2.Parameters.Add("@l", SqlDbType.NVarChar).Value = link;
            com2.Parameters.Add("@i", SqlDbType.NVarChar).Value = id;
            com2.Parameters.Add("@u", SqlDbType.NVarChar).Value = creator;
            com2.Parameters.Add("@r", SqlDbType.Int).Value = 0;
            com2.ExecuteNonQuery();

            SqlCommand com4 = new SqlCommand("CREATE TABLE " + id + "_CHARACTER(id nvarchar(max), [user] nvarchar(max),likes int,reports int,name nvarchar(max), job nvarchar(max), time DateTime)",con);
            com4.ExecuteNonQuery();
            con.Close();
            var resp = Request.CreateResponse<string>(HttpStatusCode.Created, id);
            return resp;
        }
        public HttpResponseMessage DeleteCharacter(string id, string creator)
        {
            SqlConnection con = new SqlConnection("Data Source=b471cd39-9c59-46a9-b4d9-a38b00a748aa.sqlserver.sequelizer.com;Initial Catalog=dbb471cd399c5946a9b4d9a38b00a748aa;Persist Security Info=True;User ID=szssaqjrgsgntzbv;Password=YZzoxqA7hHMpQdX5HNyHktmroFS8DjCgEmyWvk6ABTJAahan4t7fFBNEMhaYEBA6");
            string chars = "1234567890qwertyuiopasdfghjklzxcvbnm";
            con.Open();
            SqlCommand com5 = new SqlCommand("SELECT * FROM characters WHERE id = @i AND [user]=@u", con);
            com5.Parameters.Add("@i", SqlDbType.NVarChar).Value = id;
            com5.Parameters.Add("@u", SqlDbType.NVarChar).Value = creator;
            SqlDataReader r3 = com5.ExecuteReader();
            if (!r3.Read())
            {
                r3.Close();
                con.Close();
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            r3.Close();
       

            SqlCommand com2 = new SqlCommand("DELETE characters WHERE id=@i AND [user]=@u", con);
            com2.Parameters.Add("@i", SqlDbType.NVarChar).Value = id;
            com2.Parameters.Add("@u", SqlDbType.NVarChar).Value = creator;
            com2.ExecuteNonQuery();

            SqlCommand com4 = new SqlCommand("DROP TABLE " + id + "_CHARACTER", con);
            com4.ExecuteNonQuery();
            con.Close();
            var resp = Request.CreateResponse<string>(HttpStatusCode.OK, id);
            return resp;
        }
        public HttpResponseMessage GetCharacter(string id, string creator)
        {
            SqlConnection con = new SqlConnection("Data Source=b471cd39-9c59-46a9-b4d9-a38b00a748aa.sqlserver.sequelizer.com;Initial Catalog=dbb471cd399c5946a9b4d9a38b00a748aa;Persist Security Info=True;User ID=szssaqjrgsgntzbv;Password=YZzoxqA7hHMpQdX5HNyHktmroFS8DjCgEmyWvk6ABTJAahan4t7fFBNEMhaYEBA6");

            con.Open();
            SqlCommand com5 = new SqlCommand("SELECT * FROM users WHERE id = @i", con);
            com5.Parameters.Add("@i", SqlDbType.NVarChar).Value = creator;
            SqlDataReader r3 = com5.ExecuteReader();
            if (!r3.Read())
            {
                r3.Close();
                con.Close();
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            r3.Close();


            SqlCommand com2 = new SqlCommand("SELECT characters WHERE id=@i", con);
            com2.Parameters.Add("@i", SqlDbType.NVarChar).Value = id;
           SqlDataReader r2 = com2.ExecuteReader();
           if (!r2.Read())
           {
               r2.Close();
               con.Close();
               throw new HttpResponseException(HttpStatusCode.NotFound);
           }
           else
           {


               var resp = Request.CreateResponse<string>(HttpStatusCode.Created, r2.GetString(1));
                              r2.Close();
               con.Close();
               return resp;
           }
        }
    }
}
