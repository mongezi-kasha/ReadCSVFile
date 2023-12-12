using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string filePath = @"C:\Users\monge\Downloads\MyFile.csv";

        try
        {
            PrintFileContents(filePath);

            double averageAge = CalculateAverageAge(filePath);

            if (!double.IsNaN(averageAge))
            {
                Console.WriteLine($"\nThe average age is: {averageAge:F2}");
            }
            else
            {
                Console.WriteLine("\nNo valid age data found in the CSV file.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static void PrintFileContents(string filePath)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);

            Console.WriteLine("File Contents:");

            // Print header
            Console.WriteLine(lines[0]);

            // Print data rows
            for (int i = 1; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }
        }
        catch (IOException ex)
        {
            throw new IOException($"Error reading the file: {ex.Message}", ex);
        }
    }

    static double CalculateAverageAge(string filePath)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);

            // Skip header if exists
            var dataRows = lines.Skip(1);

            int totalAge = 0;
            int count = 0;

            foreach (var row in dataRows)
            {
                var columns = row.Split(',');

                // Assuming age is in the third column (index 2)
                if (columns.Length > 2 && int.TryParse(columns[2], out int age))
                {
                    totalAge += age;
                    count++;
                }
            }

            if (count > 0)
            {
                return (double)totalAge / count;
            }
            else
            {
                return double.NaN; // Indicate that no valid age data was found
            }
        }
        catch (IOException ex)
        {
            throw new IOException($"Error reading the file: {ex.Message}", ex);
        }
    }
}
