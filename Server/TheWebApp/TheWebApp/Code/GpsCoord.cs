using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Web;

namespace TheWebApp.Code
{
    public class GpsCoord
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        private GeoCoordinate geo;

        public GpsCoord(double lat, double lon)
        {
            Longitude = lon;
            Latitude = lat;
            geo = new GeoCoordinate(lat, lon);
        }

        public GpsCoord(string lon, string lat)
        {
            Longitude = double.Parse(lon);
            Latitude = double.Parse(lat);
            geo = new GeoCoordinate(Longitude, Latitude);
        }

        public double DistanceTo(GpsCoord other)
        {
           return geo.GetDistanceTo(other.geo);
        }
    }
}