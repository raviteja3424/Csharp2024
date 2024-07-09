using System;

class Square
{
    static void Main()
    {
        int[] numbers = { 6, 9, 30 };

        Console.WriteLine("Expected output:");

        foreach (int number in numbers)
        {
            int square = number * number;

            if (square > 20)
            {
                Console.WriteLine($"→ {number} - {square}");
                Console.ReadLine();
            }
        }
    }
}
