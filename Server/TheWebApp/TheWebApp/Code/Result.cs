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
        public string City;

        public Dictionary<DateTime, double> Data;

        public Result()
        {
            Data = new Dictionary<DateTime, double>();
        }

        public void AddData(string time, string value)
        {
            Data.Add(DateTime.ParseExact(time, "dd/MM/yyyy", null), Double.Parse(value));
        }
    }
}