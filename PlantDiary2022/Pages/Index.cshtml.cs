using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPlantDiary;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using PlantFeed;
using System.Net;

namespace PlantDiary2022.Pages
{
    public class IndexModel : PageModel
    {

        static readonly HttpClient client = new HttpClient();

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var task = GetData();
            List<Specimen> result = task.Result;
            ViewData["Specimens"] = result;
            SpecimenRoster.allSpecimens = result;


        }

        private async Task<List<Specimen>> GetData() {
            return await Task.Run(async () =>
            {
                string brand = "My Plant Diary";
                string inBrand = Request.Query["Brand"];
                if (inBrand != null && inBrand.Length > 0)
                {
                    brand = inBrand;
                }
                int yearStarted = 2006;
                ViewData["Brand"] = brand + " Established " + yearStarted;

                Task<HttpResponseMessage> plantTask = client.GetAsync("http://plantplaces.com/perl/mobile/viewplantsjsonarray.pl?WetTolerant=on");

                var task = client.GetAsync("http://plantplaces.com/perl/mobile/specimenlocations.pl?Lat=39.14455&Lng=-84.50939&Range=0.5&Source=location");


                var config = new ConfigurationBuilder()
                    .AddUserSecrets<Program>()
                    .Build();
                string weatherapikey = config["weatherapikey"];

                string weatherEndpoint = "https://api.weatherbit.io/v2.0/current?&city=Cincinnati&country=USA&key=" + weatherapikey;
                Task<HttpResponseMessage> weatherTask = client.GetAsync(weatherEndpoint);
                
                HttpResponseMessage result = task.Result;
                List<Specimen> specimens = new List<Specimen>();
                if (result.IsSuccessStatusCode)
                {
                    Task<string> readString = result.Content.ReadAsStringAsync();
                    string jsonString = readString.Result;
                    JSchema schema = JSchema.Parse(System.IO.File.ReadAllText("specimenschema.json"));
                    JArray jsonArray = JArray.Parse(jsonString);
                    IList<string> validationEvents = new List<string>();
                    if (jsonArray.IsValid(schema, out validationEvents))
                    {
                        specimens = Specimen.FromJson(jsonString);
                    }
                    else
                    {
                        foreach (string evt in validationEvents)
                        {
                            Console.WriteLine(evt);
                        }
                    }

                }
                HttpResponseMessage plantResult = await plantTask;
                Task<string> plantTaskString = plantResult.Content.ReadAsStringAsync();
                string plantJson = plantTaskString.Result;
                List<Plant> plants = Plant.FromJson(plantJson);

                IDictionary<long, Plant> waterLovingPlants = new Dictionary<long, Plant>();
                foreach (Plant plant in plants)
                {
                    waterLovingPlants[plant.Id] = plant;
                }
                List<Specimen> waterLovingSpecimens = new List<Specimen>();
                foreach (Specimen specimen in specimens)
                {
                    if (waterLovingPlants.ContainsKey(specimen.PlantId))
                    {
                        // it's a water loving plant.
                        waterLovingSpecimens.Add(specimen);

                    }
                }
                HttpResponseMessage weatherResponse = await weatherTask;
                Task<string> weatherReadTask = weatherResponse.Content.ReadAsStringAsync();
                string weatherJson = weatherReadTask.Result;
                
                long precip = 0;
                WeatherFeed.Weather weather = WeatherFeed.Weather.FromJson(weatherJson);
                List<WeatherFeed.Datum> weatherData = weather.Data;
                foreach(WeatherFeed.Datum weatherDatum in weatherData)
                {
                    precip = weatherDatum.Precip;
                } 
                if (precip < 1)
                {
                    ViewData["Message"] = "It's dry!  Water these plants.";
                }
                else
                {
                    ViewData["Message"] = "Rain expected.  No need to water.";
                }

                return waterLovingSpecimens;
            });
        }
    }
}