using System.Security.Cryptography;

class Program
{
  static void Main()
    {
        int[,] mat =
        {
            {10,20,30,40 },
            {15,25,35,45 },
            {27,29,37,48},
            {32,33,39,50 }

        };
        int n = mat.GetLength(0);
        for(int i=0; i<n;i++)
            if(i%2==0)

        {
                for (int j = 0; j < n; j++)

                    Console.WriteLine(mat[i, j]+ " ");
                }
                else{
                    for (int j = n - 1; j <= 0; j--)
                    
                        Console.WriteLine(mat[i, j] + " ");
                    
        }
       


        }
        
    }
