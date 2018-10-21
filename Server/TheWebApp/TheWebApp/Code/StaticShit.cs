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

        public static void Init()
        {
            Db = new DbAdaptor("yourBoy.db");
            KnownGpsCoords = Db.GetUniqueLocations();
        }
    }
}