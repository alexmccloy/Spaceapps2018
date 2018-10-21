using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheWebApp.Code
{
    [Serializable]
    public class Result
    {
        public string DataType;
        public double Latitude;
        public double Longitude;
        public string City;

        public Dictionary<DateTime, double> Data;
        public Dictionary<string, string> ExtraData;

        public Result()
        {
            Data = new Dictionary<DateTime, double>();
            ExtraData = new Dictionary<string, string>();
        }

        public void AddData(string time, string value)
        {
            Data.Add(DateTime.ParseExact(time, "d/M/yyyy", null), Double.Parse(value));
        }
    }
}