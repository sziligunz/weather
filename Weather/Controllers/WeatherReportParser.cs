﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Weather.Models;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Text.Json.Serialization;

namespace Weather.Controllers
{
    internal static class WeatherReportParser
    {
        private static JsonSerializerOptions parserOptions = new()
        {
            //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            //DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        };

        private static ModelJsonResponse DeserializeModelWeather(string input)
        {
            ModelJsonResponse? wObject = JsonSerializer.Deserialize<ModelJsonResponse>(input, parserOptions);
            if (wObject == null)
            {
                Console.WriteLine("Couldn't parse to ModelWeather object from string.");
            }

            return wObject;
        }

        public static string SerializeModelWeather(ModelJsonResponse input)
        {
            return JsonSerializer.Serialize(input, parserOptions);
        }

        public static async Task<ModelJsonResponse> getCurrentWeather()
        {
            string data = await WeatherIO.ReadIn();
            ModelJsonResponse modelJsonResponse;
            try
            {
                modelJsonResponse = WeatherReportParser.DeserializeModelWeather(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            if (modelJsonResponse == null || modelJsonResponse.list == null)
            {
                Console.WriteLine("The weather report which is provided by the weather API doesn't conatain any actual data.");
                return null;
            }
            return modelJsonResponse;
        }
    }
}