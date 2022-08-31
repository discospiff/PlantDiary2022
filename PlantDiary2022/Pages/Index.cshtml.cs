using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPlantDiary;
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
            string brand = "My Plant Diary";
            string inBrand = Request.Query["Brand"];
            if (inBrand != null && inBrand.Length > 0)
            {
                brand = inBrand;
            }
            int yearStarted = 2006;
            ViewData["Brand"] = brand + " Established " + yearStarted;

            var task = client.GetAsync("http://plantplaces.com/perl/mobile/specimenlocations.pl?Lat=39.14455&Lng=-84.50939&Range=0.5&Source=location");
            HttpResponseMessage result = task.Result;
            List<Specimen> specimens = new List<Specimen>();
            if (result.IsSuccessStatusCode)
            {
                Task<string> readString = result.Content.ReadAsStringAsync();
                string jsonString = readString.Result;
                specimens = Specimen.FromJson(jsonString);
            }
            ViewData["Specimens"] = specimens;
            

        }
    }
}