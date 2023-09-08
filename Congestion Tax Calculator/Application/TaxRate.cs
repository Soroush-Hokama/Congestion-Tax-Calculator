using Congestion_Tax_Calculator.Domain;

namespace Congestion_Tax_Calculator.Application
{
    /// <summary>
    /// get the amount for congestion tax
    /// </summary>
    public class TaxRate
    {
        private List<TaxTimeRateBase> TaxTimeRates { get; set; }

        public TaxRate(TaxRule rule)
        {
                TaxTimeRates = rule.GetTaxTimeRateList();
        }

        public TaxTimeRateBase GetTaxRateObj(DateTime dateTime)
        {
            if (TaxTimeRates != null && TaxTimeRates.Count > 0)
            {
                TimeSpan DeiredTime = dateTime.TimeOfDay;
                TaxTimeRateBase RateObj = TaxTimeRates.FirstOrDefault(x => x.StartTime <= DeiredTime && x.EndTime >= DeiredTime);
                return RateObj;
            }
            return null;    
        }
    }
}
