﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using WeatherFeed;
//
//    var weather = Weather.FromJson(jsonString);

namespace WeatherFeed
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Weather
    {
        [JsonProperty("data")]
        public List<Datum> Data { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }
    }

    public partial class Datum
    {
        [JsonProperty("wind_cdir")]
        public string WindCdir { get; set; }

        [JsonProperty("rh")]
        public long Rh { get; set; }

        [JsonProperty("pod")]
        public string Pod { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("pres")]
        public long Pres { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("ob_time")]
        public string ObTime { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("clouds")]
        public long Clouds { get; set; }

        [JsonProperty("ts")]
        public long Ts { get; set; }

        [JsonProperty("solar_rad")]
        public long SolarRad { get; set; }

        [JsonProperty("state_code")]
        public string StateCode { get; set; }

        [JsonProperty("city_name")]
        public string CityName { get; set; }

        [JsonProperty("wind_spd")]
        public double WindSpd { get; set; }

        [JsonProperty("slp")]
        public double Slp { get; set; }

        [JsonProperty("wind_cdir_full")]
        public string WindCdirFull { get; set; }

        [JsonProperty("sunrise")]
        public string Sunrise { get; set; }

        [JsonProperty("app_temp")]
        public double AppTemp { get; set; }

        [JsonProperty("dni")]
        public long Dni { get; set; }

        [JsonProperty("vis")]
        public long Vis { get; set; }

        [JsonProperty("sources")]
        public List<string> Sources { get; set; }

        [JsonProperty("h_angle")]
        public long HAngle { get; set; }

        [JsonProperty("dewpt")]
        public double Dewpt { get; set; }

        [JsonProperty("snow")]
        public long Snow { get; set; }

        [JsonProperty("aqi")]
        public long Aqi { get; set; }

        [JsonProperty("dhi")]
        public long Dhi { get; set; }

        [JsonProperty("wind_dir")]
        public long WindDir { get; set; }

        [JsonProperty("elev_angle")]
        public double ElevAngle { get; set; }

        [JsonProperty("ghi")]
        public long Ghi { get; set; }

        [JsonProperty("precip")]
        public long Precip { get; set; }

        [JsonProperty("sunset")]
        public string Sunset { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("uv")]
        public long Uv { get; set; }

        [JsonProperty("datetime")]
        public string Datetime { get; set; }

        [JsonProperty("temp")]
        public double Temp { get; set; }

        [JsonProperty("weather")]
        public WeatherClass Weather { get; set; }

        [JsonProperty("station")]
        public string Station { get; set; }
    }

    public partial class WeatherClass
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public partial class Weather
    {
        public static Weather FromJson(string json) => JsonConvert.DeserializeObject<Weather>(json, WeatherFeed.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Weather self) => JsonConvert.SerializeObject(self, WeatherFeed.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
