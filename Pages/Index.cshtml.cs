using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CreditApplicationRESTservice.Pages
{
    public class IndexModel : PageModel
    {
        private HttpClient _client;

        public string creditAmount { get; set; }
        public string term { get; set; }
        public string preExCreditAmount { get; set; }
        public string descision { get; set; }
        public string interestRatePercentage { get; set; }

        public IndexModel(HttpClient client)
        {
            _client = client;
        }
        
        public async Task<IActionResult> OnGetAsync(string creditAmount, string term, string preExCreditAmount)
        {
            var response = await _client.GetAsync("https://localhost:44334/api/creditapplication/get/" + creditAmount + "/" 
                + term + "/" + preExCreditAmount);

            var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
            var content = await response.Content.ReadAsStringAsync();
            Response finalResponse = new Response();
            finalResponse = JsonConvert.DeserializeObject<Response>(content, settings);

            if (finalResponse == null)
            {
                return Page();
            }
            else
            {
                descision = finalResponse.descision;
                interestRatePercentage = finalResponse.interestRatePercentage;
                return Page();
            }
        }
        
    }
}
