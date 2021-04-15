using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;

namespace CreditApplicationRESTservice.Controllers
{
    [ApiController]
    public class CreditApplicationController : ControllerBase
    {
        public class CreditRequest
        {
            public float creditAmount { get; set; }
            public float term { get; set; }
            public float preExCreditAmmount { get; set; }

        }

        private readonly ILogger<CreditApplicationController> _logger;

        public CreditApplicationController(ILogger<CreditApplicationController> logger)
        {
            _logger = logger;
        }

        public float totalFutureDebt;


        [Route("api/[controller]/get/{creditamount}/{term}/{preexcreditamount}")]
        [HttpGet]
        public IActionResult Get(float creditamount, int term, float preexcreditamount)
        {
            if (creditamount < 0 || term < 1 || preexcreditamount < 0)
                return BadRequest("Parameters used are not valid");

            Response creditResponse = new Response();

            totalFutureDebt = (creditamount - preexcreditamount) * term;

            if (creditamount < 2000 || creditamount > 69000)
                creditResponse.descision = "No";
            else
                creditResponse.descision = "Yes";
            if (totalFutureDebt < 20000)
                creditResponse.interestRatePercentage = "3";
            else if (totalFutureDebt >= 20000 && totalFutureDebt <= 39000)
                creditResponse.interestRatePercentage = "4";
            else if (totalFutureDebt >= 40000 && totalFutureDebt <= 59000)
                creditResponse.interestRatePercentage = "5";
            else
                creditResponse.interestRatePercentage = "6";

            string JSONresult = JsonConvert.SerializeObject(creditResponse);

            return Ok(JSONresult);
        }
    }
}
