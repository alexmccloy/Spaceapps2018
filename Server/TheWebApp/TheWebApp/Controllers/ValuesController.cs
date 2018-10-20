using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Hosting;
using System.Web.Http;

namespace TheWebApp.Controllers
{
    public class ValuesController : ApiController
    {
        private SQLiteConnection dbConnection;

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            using (dbConnection = new SQLiteConnection($"Data Source = {HostingEnvironment.MapPath(@"~/App_Data/test.db")}.; Version = 3;"))
            {
                dbConnection.Open();
                string query = "SELECT * FROM testTable";
                var command = new SQLiteCommand(query, dbConnection);
                var reader = command.ExecuteReader();

                StringBuilder sb = new StringBuilder();

                while(reader.Read())
                {
                    sb.Append($"Col1: {reader["col1"]}, Col2: {reader["col2"]}");
                }

                return sb.ToString();
            }
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
