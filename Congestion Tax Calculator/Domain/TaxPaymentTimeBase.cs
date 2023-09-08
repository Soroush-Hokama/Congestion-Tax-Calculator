namespace Congestion_Tax_Calculator.Domain
{
    public class TaxTimeRateBase
    {
        /// <summary>
        /// Start time to calculate tax amount per day
        /// </summary>
        public TimeSpan StartTime { get; set; }
        /// <summary>
        /// End Tie to calculate tax amount per day
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// the amount of tax per time in day
        /// </summary>
        public decimal Amount { get; set; }
    }
}
