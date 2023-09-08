using Congestion_Tax_Calculator.Domain;
using System.Reflection.Metadata.Ecma335;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Congestion_Tax_Calculator.Application
{
    /// <summary>
    /// we can make a class to mange all messages.
    /// </summary>
    public class TaxCalculator
    {
        /// <summary>
        /// define the ruled based on each country at first
        /// </summary>
        private TaxRule Rule { get; set; }
        /// <summary>
        /// Hours and amounts for congestion tax vary based on the rule passed for each country.
        /// </summary>
        private TaxRate Rate { get; set; }
        public TaxCalculator(TaxRule taxRule)
        {
            Rule = taxRule;
        }
        public dynamic GetTax(List<DateTime> clientDateTimes)
        {

            // get the Hours and amounts for congestion tax
            Rate = new TaxRate(Rule);
            if (Rule == null)
                return "the rate of tax is not provided";
            DateTime intervalStart = clientDateTimes[0];
            decimal totalTax = 0;
            foreach (DateTime date in clientDateTimes)
            {
                if (Rule.CheckDateRule(date))
                    continue;
                TaxTimeRateBase nextFee = Rate.GetTaxRateObj(date);
                if (nextFee == null)
                    continue;
                double minutesPassed = (date - intervalStart).TotalMinutes;
                if (minutesPassed <= Rule.GetHourRule)
                    totalTax = Math.Max(totalTax, nextFee.Amount);
                else
                {
                    intervalStart = date;
                    decimal tax = nextFee.Amount;
                    totalTax += tax;
                }
            }
            return Rule.CheckMaxAmountPerDateRule(totalTax);
        }
    }
}
