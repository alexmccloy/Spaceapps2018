﻿using System;
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

        public int DataCountBefore;
        public int DataCountAfter;
        public int Month;

        public Result()
        {
            Data = new Dictionary<DateTime, double>();
            ExtraData = new Dictionary<string, string>();
        }

        public void AddData(string time, string value)
        {
            DateTime key = DateTime.ParseExact(time, "M/d/yyyy", null);
            if (!Data.ContainsKey(key)) {
                Data.Add(key, Double.Parse(value));
            }
        }

        private string GetCardTitle()
        {
            string month = DateTime.Today.ToString("MMMM");
            if (ExtraData.ContainsKey("title")) return ExtraData["title"];

            if (DataType == TableType.temperature_maximum.ToString()) return $"Temperature - Monthly Highest {month}";
            if (DataType == TableType.temperature_meanmax.ToString()) return $"Temperature - Monthly Average Maximum {month}";
            if (DataType == TableType.temperature_meanmin.ToString()) return $"Temperature - Monthly Average Minimum {month}";
            if (DataType == TableType.rain_total.ToString()) return "Rainfall - Monthly Total";

            return "Card";
        }

        public void PREPARETOBESERIALISED(int month)
        {
            Month = month;
            DataCountBefore = Data.Count();
            CardTitle = GetCardTitle();

            //filter data so that it is only for this month
            Dictionary<DateTime, double> newData = new Dictionary<DateTime, double>();
            List<DateTime> toRemove = new List<DateTime>();
            foreach(var key in Data.Keys)
            {
                if (key.Month != month)
                {
                    //newData.Add(key, Data[key]);
                    toRemove.Add(key);
                }
            }

            foreach(var a in toRemove)
            {
                Data.Remove(a);
            }

            DataCountAfter = Data.Count();
        }
    }
}