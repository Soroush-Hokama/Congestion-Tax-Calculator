
using System.Globalization;
using System.Linq;
using Congestion_Tax_Calculator.Domain;

namespace Congestion_Tax_Calculator.Application
{
    /// <summary>
    /// this class was designed to define tax rule dynamically
    /// I think each below list get from database
    /// </summary>
    public class TaxRule : TaxRuleBase
    {
        public TaxRule(string location,
                       DateTime[] publicHolidays,
                       DateTime[] duringSpecialMonth,
                       DateTime[] beforePublicHoliDay,
                       DayOfWeek[] freeDayInWeeks,
                       List<TaxTimeRateBase> taxTimeRates, 
                       List<Vehicle> freeVehicleList,
                       decimal maxAmountTaxPerDay,
                       int hourLimit)
        {
            Location = location;
            FreeDayOfWeekList = freeDayInWeeks;
            TaxTimeRates = taxTimeRates;
            PublicHolidays = publicHolidays;
            DuringSpecialMonth = duringSpecialMonth;
            BeforePublicHoliDay = beforePublicHoliDay;
            FreeVehicleList = freeVehicleList;
            MaxAmountTaxPerDay = maxAmountTaxPerDay;
            HourLimit = hourLimit;
        }
        /// <summary>
        /// checking the rules of given datetime
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool CheckDateRule(DateTime date)
        {
            // Check if the input date falls on a holiday during the week
            if (PublicHolidays.Contains(date)) return true;

            // Check if the input date falls on a day designated as 'Free'
            if (FreeDayOfWeekList.Contains(date.DayOfWeek)) return true;
           
            //check if the input Check if the input date falls on a Special day on month
            if (DuringSpecialMonth.Contains(date)) return true;

            // check input time between Tax payment time
            TimeSpan timeOfDay = date.TimeOfDay;
            foreach (var TimeObj in TaxTimeRates)
            {
                if (timeOfDay >= TimeObj.StartTime && timeOfDay <= TimeObj.EndTime)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Check if the given vehicle is included in the FreeVehicleList or not
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public bool CheckFreeVehicleRule(Vehicle vehicle)
        {
            //check if vehicle  include in FreeVehicleList List
            foreach(var obj in FreeVehicleList)
                if(obj.Name == vehicle.Name) return true;
            return false;
        }
        /// <summary>
        /// check the rate of each supplied time rule which doesn't greater than The maximum amount per day and vehicle
        /// </summary>
        /// <returns></returns>
        public bool CheckTaxTimeRatesRule()
        {
            if(TaxTimeRates.Count == 0)
                return true;    
            foreach (var rate in TaxTimeRates)
            {
                if(rate.Amount > MaxAmountTaxPerDay)
                    return true;
            }
            return false;   
        }
        /// <summary>
        /// Obtain rates for congestion tax if the rule is passed.
        /// </summary>
        /// <returns></returns>
        public List<TaxTimeRateBase> GetTaxTimeRateList()
        {
            if (!CheckTaxTimeRatesRule())
                return TaxTimeRates;
            return default;
        }

        public int GetHourRule
        {
            get
            {
                if (HourLimit > 0)
                    return HourLimit;
                return 0;
            }
        }

        /// <summary>
        /// Chech calculated tax which isn't greater than The maximum amount per day and vehicle
        /// </summary>
        /// <param name="maxAmount"></param>
        /// <returns></returns>
        public decimal CheckMaxAmountPerDateRule(decimal maxAmount)
        {
            if (maxAmount > MaxAmountTaxPerDay)
                return MaxAmountTaxPerDay;
            return maxAmount;
        }
    }
}
