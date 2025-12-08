using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter number of students: ");
        int n = int.Parse(Console.ReadLine());

        int[] marks = new int[n];
        Console.WriteLine("\nEnter marks:");
        for (int i = 0; i < n; i++)
        {
            marks[i] = int.Parse(Console.ReadLine());
        }
        int[] backup = (int[])marks.Clone();
        Console.WriteLine("\nMarks entered:");
        foreach (int m in marks)
            Console.Write(m + " ");
        Array.Sort(marks);

        Console.WriteLine("\n\nSorted marks:");
        foreach (int m in marks)
            Console.Write(m + " ");
        int total = 0;
        foreach (int m in marks)
            total += m;

        Console.WriteLine($"\n\nTotal marks = {total}");
        Console.WriteLine("Highest mark = " + marks[n - 1]);
        Console.WriteLine("Lowest mark = " + marks[0]);
        Console.Write("\nEnter a mark to search: ");
        int search = int.Parse(Console.ReadLine());

        bool found = false;
        foreach (int m in marks)
        {
            if (m == search)
            {
                found = true;
                break;
            }
        }
        Console.WriteLine(found ? "Mark found ✔" : "Mark not found ✘");
        Console.WriteLine("\nEnter marks again to compare:");
        int[] secondMarks = new int[n];
        for (int i = 0; i < n; i++)
        {
            secondMarks[i] = int.Parse(Console.ReadLine());
        }

        bool same = true;
        for (int i = 0; i < n; i++)
        {
            if (marks[i] != secondMarks[i])
            {
                same = false;
                break;
            }
        }
        Console.WriteLine(same ? "\nBoth sets are identical ✔" : "\nBoth sets are different ✘");
    }
}
