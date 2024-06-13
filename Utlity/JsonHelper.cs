using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using StockMarketApp.Models;
namespace StockMarketApp.Utlity
{
    public class JsonHelper
    {
        private static readonly string JsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "stock_market_data.json");
        private static readonly string CountJsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "count.json");

        public static List<T> ReadFromJsonFile<T>()
        {
            using StreamReader file = File.OpenText(JsonFilePath);
            JsonSerializer serializer = new JsonSerializer();
            return (List<T>)serializer.Deserialize(file, typeof(List<T>));
        }
        public static CountStock ReadCountJsonFile()
        {
            using StreamReader file = File.OpenText(CountJsonFilePath);
            JsonSerializer serializer = new JsonSerializer();
            return (CountStock)serializer.Deserialize(file, typeof(CountStock));
        }


        public static void WriteToJsonFile<T>(List<T> data)
        {
            using StreamWriter file = File.CreateText(JsonFilePath);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, data);
        }
        public static void WriteToCountJsonFile(CountStock data)
        {
            using StreamWriter file = File.CreateText(CountJsonFilePath);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, data);
        }
    }
}
