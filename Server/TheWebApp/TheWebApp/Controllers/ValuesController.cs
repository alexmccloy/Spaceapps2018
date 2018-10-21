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
using TheWebApp.Code;

namespace TheWebApp.Controllers
{
    public class ValuesController : ApiController
    {
        private SQLiteConnection dbConnection;

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "You got the default Get", "IDIOT" };
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

        /// <summary>
        /// Return all the available data closest to the given coordinate
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        public IList<Result> Get(double lon, double lat)
        {
            GpsCoord requestedGps = new GpsCoord(lat, lon);
            List<Result> results = new List<Result>();

            //Get best location
            double minDist = Double.MaxValue;
            string bestLocation = String.Empty;
            foreach (var pair in StaticShit.KnownGpsCoords)
            {
                double dist = requestedGps.DistanceTo(pair.Value);
                if (dist < minDist)
                {
                    minDist = dist;
                    bestLocation = pair.Key;
                }
            }

            //Get all available data
            foreach (TableType type in Util.GetAllItems<TableType>())
            {
                results.Add(StaticShit.Db.GetResultFor(type, bestLocation));
            }

            StaticShit.UsefulInformation.ToList().ForEach(item => results.Add(item));

            return results;
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
