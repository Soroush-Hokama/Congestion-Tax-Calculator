using Congestion_Tax_Calculator.Application;
using Congestion_Tax_Calculator.Domain;
using Congestion_Tax_Calculator.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.FileProviders;
using System.Data;

namespace Congestion_Tax_Calculator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }


        public IActionResult OnPost()
        {
            /// get content.json file from wwwroot/Datasource folder and deserialize object
            TaxRuleBase BaseObj = JsonDataContent.GetDataSourceAsJson();

            if (BaseObj == null)
                return new JsonResult("Error in converting the input file");

            // getting desired vehicle from client (eg. User input field)  
            Vehicle vehicleObj = new Vehicle()
            {
                Name = "Arizo"
            };

            // getting desired data time to calculate tax congestion
            List<DateTime> InputdateTimes = new List<DateTime>
        {
            DateTime.ParseExact("2013-01-14 21:00:00", "yyyy-MM-dd HH:mm:ss", null),
            DateTime.ParseExact("2013-01-15 21:00:00", "yyyy-MM-dd HH:mm:ss", null),
            DateTime.ParseExact("2013-02-07 06:10:27", "yyyy-MM-dd HH:mm:ss", null),
            DateTime.ParseExact("2013-02-07 06:23:27", "yyyy-MM-dd HH:mm:ss", null),
            DateTime.ParseExact("2013-02-07 15:27:00", "yyyy-MM-dd HH:mm:ss", null),
            DateTime.ParseExact("2013-02-08 06:27:00", "yyyy-MM-dd HH:mm:ss", null),
            DateTime.ParseExact("2013-02-08 06:35:10", "yyyy-MM-dd HH:mm:ss", null),
            DateTime.ParseExact("2013-02-08 07:34:00", "yyyy-MM-dd HH:mm:ss", null),
            DateTime.ParseExact("2013-02-08 15:29:00", "yyyy-MM-dd HH:mm:ss", null),
            DateTime.ParseExact("2013-02-08 15:47:00", "yyyy-MM-dd HH:mm:ss", null),
            DateTime.ParseExact("2013-02-08 16:01:00", "yyyy-MM-dd HH:mm:ss", null),
            DateTime.ParseExact("2013-02-08 16:48:00", "yyyy-MM-dd HH:mm:ss", null),
            DateTime.ParseExact("2013-02-08 17:49:00", "yyyy-MM-dd HH:mm:ss", null),
            DateTime.ParseExact("2013-02-08 18:29:00", "yyyy-MM-dd HH:mm:ss", null),
            DateTime.ParseExact("2013-02-08 18:35:00", "yyyy-MM-dd HH:mm:ss", null)

        };
            if (InputdateTimes == null || InputdateTimes.Count == 0)
                return new JsonResult("please define the datetimes to calculate");
            //(LSP) 
            TaxRule RuleObj = new TaxRule(BaseObj.Location,
                                          BaseObj.PublicHolidays,
                                          BaseObj.DuringSpecialMonth,
                                          BaseObj.BeforePublicHoliDay,
                                          BaseObj.FreeDayOfWeekList,
                                          BaseObj.TaxTimeRates,
                                          BaseObj.FreeVehicleList,
                                          BaseObj.MaxAmountTaxPerDay,
                                          BaseObj.HourLimit);
      

            //check the rules based on defined country in json file at first
            if (RuleObj == null)
                return new JsonResult("please make Rules in json file ");
            if (RuleObj.CheckFreeVehicleRule(vehicleObj))
                return new JsonResult("The " + vehicleObj.Name + " is exempt from taxation");
            if (RuleObj.CheckTaxTimeRatesRule())
                return new JsonResult("the rate which you definded is greater than The maximum amount per day and vehicle.");
            if (RuleObj.HourLimit == 0)
                return new JsonResult("Please set HourLimit in the JSON file (a value of 0 is not acceptable.");


            //main operation to calculate tax congestion
            TaxCalculator calculatorObj = new TaxCalculator(RuleObj);

            var obj = calculatorObj.GetTax(InputdateTimes.OrderBy(x=>x).ToList());
            if (obj is string)
                return new JsonResult("the error when calculating tax " + obj);
            if (obj is decimal)
                return new JsonResult("the tax of " + vehicleObj.Name  + " vehicle in " + BaseObj.Location + " is " + obj);
            return Page();
        }
    }
}