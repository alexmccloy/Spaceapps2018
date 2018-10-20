using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheWebApp.Code
{
    public class Result
    {
        public string DataType;
        public double Latitude;
        public double Longitude;
        public double City;

        public IDictionary<DateTime, double> Data;
    }
}