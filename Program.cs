
namespace FintechAndDeviceMonitor
{
    class Program
    {
        static void Main(string[] args)
        {

            string transactionID = "TXN123ABC456";
            decimal amount = 1000000.75m;
            bool isInternational = true;
            double customerRating = 4.75;
            DateTime transactionTimestamp = DateTime.Now;
            long rewardPoints = 5000000;

            Console.WriteLine("=== Credit Card Transaction Details ===");
            Console.WriteLine($"Transaction ID: {transactionID}");
            Console.WriteLine($"Amount: ₹{amount}");
            Console.WriteLine($"Is International: {isInternational}");
            Console.WriteLine($"Customer Rating: {customerRating}");
            Console.WriteLine($"Transaction Timestamp: {transactionTimestamp}");
            Console.WriteLine($"Reward Points: {rewardPoints}");
            Console.WriteLine();

            Console.WriteLine("=== Starting Device Health Monitoring System ===");
            TemperatureModule.RunTemperatureModule();
        }
    }

    class TemperatureModule
    {
        public static void RunTemperatureModule()
        {
            Console.WriteLine("Temperature Module is running...");

            VibrationModule.RunVibrationModule();

            double currentTemp = 75.5;
            Console.WriteLine($"Current Temperature: {currentTemp} °C");
        }
    }

    class VibrationModule
    {
        public static void RunVibrationModule()
        {
            Console.WriteLine("Vibration Module Helper is running...");

            double vibrationLevel = 0.03;
            Console.WriteLine($"Current Vibration Level: {vibrationLevel} mm/s");
        }
    }
}