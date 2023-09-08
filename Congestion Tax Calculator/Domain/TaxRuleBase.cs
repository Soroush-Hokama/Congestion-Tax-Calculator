namespace Congestion_Tax_Calculator.Domain
{
    public class TaxRuleBase
    {
        /// <summary>
        /// the country name to get rule
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// the list of free vehicle based on outside content (json file , sqllite etc)
        /// </summary>
        public List<Vehicle> FreeVehicleList { get; set; }
        /// <summary>
        /// Hours and amounts for congestion tax based on outside content
        /// </summary>
        public List<TaxTimeRateBase> TaxTimeRates { get; set; }
        /// <summary>
        ///  The maximum amount tax per day and vehicle based on outside content
        /// </summary>
        public decimal MaxAmountTaxPerDay { get; set; }

        /// <summary>
        /// to dynamic a vehicle that passes several tolling stations within a Hour is only taxed once
        /// </summary>
        public int HourLimit { get; set; }


        ///// <summary>
        ///// all dates which the tax fee is free based on outside content
        ///// </summary>
        //public List<DateTime> FreeDates { get; set; }
        /// <summary>
        /// all day of weeks which the tax fee is free based on outside content
        /// </summary>
        public DayOfWeek[] FreeDayOfWeekList { get; set; }

        /// <summary>
        /// check special date not public holyday
        /// </summary>
        public DateTime[] PublicHolidays { get; set; }

        /// <summary>
        /// check special date not during special month 
        /// </summary>
        public DateTime[] DuringSpecialMonth { get; set; }

        /// <summary>
        /// check special date not Before Public HoliDay
        /// </summary>
        public DateTime[] BeforePublicHoliDay { get;set; }


    }
}
