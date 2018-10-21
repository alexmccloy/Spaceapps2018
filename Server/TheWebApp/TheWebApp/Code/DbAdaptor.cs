using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
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
            dbConnection.Open();
        }

        public IDictionary<string, GpsCoord> GetUniqueLocations()
        {
            //dbConnection.Open();
            List<string> tableNames = new List<string>();

            Util.GetAllItems<TableType>().ToList().ForEach(item => tableNames.Add(item.ToString()));

            Dictionary<string, GpsCoord> locations = new Dictionary<string, GpsCoord>();

            foreach (string tableName in tableNames)
            {
                SQLiteDataReader reader;
                lock (dbConnection)
                {
                    string query = $"SELECT DISTINCT location,latitude,longitude FROM {tableName}";
                    var command = new SQLiteCommand(query, dbConnection);
                    reader = command.ExecuteReader();
                }

                while(reader.Read())
                {
                    string locationName = reader["location"].ToString();
                    if (!locations.ContainsKey(locationName))
                    {
                        locations.Add(locationName, new GpsCoord(reader["latitude"].ToString(), reader["longitude"].ToString()));
                    }
                }
            }
            //dbConnection.Close();

            return locations;
        }

        public Result GetResultFor(TableType table, string locationName)
        {
            //dbConnection.Open();
            string query = $"SELECT itsTheDate,yaboythevalue FROM {table.ToString()} WHERE location = '{locationName}'";
            SQLiteDataReader reader;

            Result result = new Result()
            {
                City = locationName,
                DataType = table.ToString(),
            };

            lock(dbConnection)
            {
                var command = new SQLiteCommand(query, dbConnection);
                reader = command.ExecuteReader();
            }

            while(reader.Read())
            {
                string a = reader["itsTheDate"].ToString();
                result.AddData(reader["itsTheDate"].ToString().Split(' ')[0], reader["yaboythevalue"].ToString());
            }

            //dbConnection.Close();
            return result;
        }

    }
}