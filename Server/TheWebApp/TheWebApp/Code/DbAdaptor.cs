using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace TheWebApp.Code
{
    public class DbAdaptor
    {
        private SQLiteConnection dbConnection;

        public DbAdaptor(string dbName)
        {
            dbConnection = new SQLiteConnection(($"Data Source = {HostingEnvironment.MapPath($"~/App_Data/{dbName}")}.; Version = 3;"));
        }

        public IDictionary<string, GpsCoord> GetUniqueLocations()
        {
            List<string> tableNames = new List<string>() { "meanmax" };

            Dictionary<string, GpsCoord> locations = new Dictionary<string, GpsCoord>();

            foreach (string tableName in tableNames)
            {
                string query = $"SELECT DISTINCTBY latitude,longitude FROM {tableName}";
                var command = new SQLiteCommand(query, dbConnection);
                var reader = command.ExecuteReader();

                while(reader.Read())
                {
                    string locationName = reader["location"].ToString();
                    if (!locations.ContainsKey(locationName))
                    {
                        locations.Add(locationName, new GpsCoord(reader["latitude"].ToString(), reader["longitude"].ToString()));
                    }
                }
            }

            return locations;
        }
    }
}