using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter size of matrix (n x n): ");
        int n = int.Parse(Console.ReadLine());

        int[,] matrix = new int[n, n];


        Console.WriteLine("\nEnter matrix elements:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write($"Enter element [{i},{j}]: ");
                matrix[i, j] = int.Parse(Console.ReadLine());
            }
        }

        int primarySum = 0, secondarySum = 0;

        for (int i = 0; i < n; i++)
        {
            primarySum += matrix[i, i];
            secondarySum += matrix[i, n - i - 1];
        }

        int absoluteDifference = Math.Abs(primarySum - secondarySum);

        Console.WriteLine($"\nPrimary diagonal sum = {primarySum}");
        Console.WriteLine($"Secondary diagonal sum = {secondarySum}");
        Console.WriteLine($"Absolute difference = {absoluteDifference}");
    }
}
