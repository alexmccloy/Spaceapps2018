using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheWebApp.Controllers;

namespace TheWebApp.Code
{
    public static class StaticShit
    {
        public static IDictionary<string, GpsCoord> KnownGpsCoords { get; private set; }
        public static DbAdaptor Db { get; private set; }
        public static IList<Result> UsefulInformation { get; private set; }

        public static void Init()
        {
            Db = new DbAdaptor("yourBoy.db");
            KnownGpsCoords = Db.GetUniqueLocations();
            UsefulInformation = GenerateUsefulInformation();
        }

        private static IList<Result> GenerateUsefulInformation()
        {
            var results = new List<Result>();

            var tempResult = new Result()
            {
                City = "ALL",
                DataType = "url_temperature",
            };
            tempResult.ExtraData.Add("title", "CO2 Earth - GLobal Warming Update");
            tempResult.ExtraData.Add("url", @"https://www.co2.earth/global-warming-update");
            tempResult.ExtraData.Add("blurb", @"The ten warmest September global land and ocean surface temperatures have occurred since 2003, with the last five years (2014–2018) comprising the five warmest Septembers on record.");
            results.Add(tempResult);

            tempResult = new Result()
            {
                City = "ALL",
                DataType = "url_temperature"
            };
            tempResult.ExtraData.Add("title", "Climate Change: The Science");
            tempResult.ExtraData.Add("url", @"https://www.greenpeace.org/usa/global-warming/climate-science/");
            tempResult.ExtraData.Add("image", @"http://www.greenpeace.org/usa/wp-content/uploads/2015/06/GP01C26-extreme-weather.jpg");
            results.Add(tempResult);

            return results;
        }
    }
}