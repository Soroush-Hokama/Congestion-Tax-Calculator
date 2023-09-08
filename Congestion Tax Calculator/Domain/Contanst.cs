namespace Congestion_Tax_Calculator.Domain
{
    public class Contanst
    {
        public enum TollFreeVehicles
        {
            Motorcycle = 0,
            Tractor = 1,
            Emergency = 2,
            Diplomat = 3,
            Foreign = 4,
            Military = 5
        }

        public enum ContentType
        {
            JsonFile = 0,
            SqlLite = 1
        }

    }
}
