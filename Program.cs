class Program
{
    static void Main()
    {
        string day = "Wednesday";

        switch (day)
        {
            case "Monday":
            case "Tuesday":
            case "Wednesday":
            case "Thursday":
            case "Friday":
                Console.WriteLine("Weekdays");
                break;
            case "Saturday":
            case "Sunday":
                Console.WriteLine("Weekend");
                break;

            default:
                Console.WriteLine("Invalid day");
                break;
        }
    }
}

