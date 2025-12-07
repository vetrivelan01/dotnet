
class Program
{
    static void Main()
    {
        Console.Write("Enter matrix size (n): ");
        int n = int.Parse(Console.ReadLine());

        int[,] mat = new int[n, n];

        Console.WriteLine("\nEnter matrix elements:");

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write($"mat[{i},{j}] = ");
                mat[i, j] = int.Parse(Console.ReadLine());
            }
        }

        Console.WriteLine("\nSnake Pattern Output:");

        for (int i = 0; i < n; i++)
        {
            if (i % 2 == 0) 
            {
                for (int j = 0; j < n; j++)
                    Console.Write(mat[i, j] + " ");
            }
            else 
            {
                for (int j = n - 1; j >= 0; j--)
                    Console.Write(mat[i, j] + " ");
            }
        }
    }
}
