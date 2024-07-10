using System;
using System.IO;

class Textfile
{
    static void Main()
    {
        Console.WriteLine("Enter the file path:");
        string filePath = Console.ReadLine();
        Console.WriteLine("Enter the text to append:");
        string appendText = Console.ReadLine(); 

        try
        {
            using (StreamWriter sw = File.Exists(filePath) ? File.AppendText(filePath) : File.CreateText(filePath))
            {
                sw.WriteLine(appendText); 
            }

            Console.WriteLine($"Text '{appendText}' appended to '{filePath}'.");

            Console.WriteLine($"Contents of '{filePath}':");
            using (StreamReader sr = File.OpenText(filePath))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
