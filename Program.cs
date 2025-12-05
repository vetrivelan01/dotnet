using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter number of students: ");
        int n = int.Parse(Console.ReadLine());

        int[] marks = new int[n];
        int[] backup = new int[n];

   
        for (int i = 0; i < n; i++)
        {
            Console.Write($"Enter mark {i + 1}: ");
            marks[i] = int.Parse(Console.ReadLine());
            backup[i] = marks[i]; // Backup copy
        }

      
        Console.WriteLine("\nMarks Entered:");
        foreach (int m in marks)
            Console.Write(m + " ");

        // Sort and display sorted marks
        Array.Sort(marks);
        Console.WriteLine("\n\nSorted Marks:");
        foreach (int m in marks)
            Console.Write(m + " ");

   
        int total = 0;
        foreach (int m in marks)
            total += m;

        Console.WriteLine($"\n\nTotal Marks: {total}");
        Console.WriteLine($"Highest Mark: {marks[n - 1]}");
        Console.WriteLine($"Lowest Mark: {marks[0]}");

       
        Console.Write("\nEnter a mark to search: ");
        int search = int.Parse(Console.ReadLine());

        if (Array.Exists(marks, m => m == search))
            Console.WriteLine("Mark Found!");
        else
            Console.WriteLine("Mark Not Found!");

 
        Console.Write("\nEnter number of marks for second set: ");
        int mCount = int.Parse(Console.ReadLine());
        int[] secondSet = new int[mCount];

        for (int i = 0; i < mCount; i++)
        {
            Console.Write($"Enter mark {i + 1}: ");
            secondSet[i] = int.Parse(Console.ReadLine());
        }

        bool same = true;

        if (marks.Length == secondSet.Length)
        {
            for (int i = 0; i < marks.Length; i++)
            {
                if (backup[i] != secondSet[i])
                {
                    same = false;
                    break;
                }
            }
        }
        else
            same = false;

        Console.WriteLine("\nComparison Result:");
        Console.WriteLine(same ? "Both sets are IDENTICAL." : "Both sets are DIFFERENT.");
    }
}
