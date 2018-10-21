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

        public string CardTitle;

        public Result()
        {
            Data = new Dictionary<DateTime, double>();
            ExtraData = new Dictionary<string, string>();
        }

        public void AddData(string time, string value)
        {
            Data.Add(DateTime.ParseExact(time, "d/M/yyyy", null), Double.Parse(value));
        }

        private string GetCardTitle()
        {
            if (ExtraData.ContainsKey("title")) return ExtraData["title"];

            if (DataType == TableType.temperature_maximum.ToString()) return "Temperature - Monthly Highest";
            if (DataType == TableType.temperature_meanmax.ToString()) return "Temperature - Monthly Average Maximum";
            if (DataType == TableType.temperature_meanmin.ToString()) return "Temperature - Monthly Average Minimum";

            return "Card";
        }

        public void PREPARETOBESERIALISED()
        {
            CardTitle = GetCardTitle();
        }
    }
}