using System;

class Cricket
{
    public void Pointscalculation(int no_of_matches)
    {
        int[] scores = new int[no_of_matches];
        int sum = 0;
        float average;

        Console.WriteLine($"Enter scores for {no_of_matches} matches:");

        for (int i = 0; i < no_of_matches; i++)
        {
            Console.Write($"Match {i + 1}: ");
            scores[i] = Convert.ToInt32(Console.ReadLine());
            sum += scores[i];
        }

        if (no_of_matches > 0)
        {
            average = (float)sum / no_of_matches;
        }
        else
        {
            average = 0;
        }

        Console.WriteLine($"Sum of the scores: {sum}");
        Console.WriteLine($"Average of the scores: {average:F2}");
    }
}

class Program
{
    static void Main()
    {
        Cricket cricket = new Cricket();

        Console.Write("Enter the number of matches conducted: ");
        int numMatches = Convert.ToInt32(Console.ReadLine());

        cricket.Pointscalculation(numMatches);

        Console.ReadLine();
    }
}
