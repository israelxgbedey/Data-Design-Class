using System;
using System.Collections.Generic;
using System.IO;

// Define an interface for file parsers
public interface IFileParser
{
    void ParseFile(string filePath);
}

// Create a class for parsing CSV files
public class CsvFileParser : IFileParser
{
    public void ParseFile(string filePath)
    {
        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Initialize a line count for error reporting
            int lineCount = 1;

            // Read the file line by line until the end is reached
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine(); // Read a line from the file
                    string[] fields = line.Split(','); // Split the line by commas (CSV)

                    // Check if there are at least 6 fields
                    if (fields.Length >= 6)
                    {
                        // Get the directory where the source file is located
                        string sourceDirectory = Path.GetDirectoryName(filePath);

                        // Create the output file path in the same directory
                        string outputFilePath = Path.Combine(sourceDirectory, Path.GetFileNameWithoutExtension(filePath) + "_out.txt");

                        // Create an output file for writing
                        using (StreamWriter writer = new StreamWriter(outputFilePath, true))
                        {
                            // Write the parsed data to the output file
                            writer.WriteLine($"Line#{lineCount} :Field#1={fields[0]} ==> Field#2={fields[1]} ==> Field#3={fields[2]} ==> Field#4={fields[3]} ==> Field#5={fields[4]} ==> Field#6={fields[5]}");
                        }

                        // Success message
                        Console.WriteLine($"Processed {Path.GetFileName(filePath)} successfully.");
                    }
                    else
                    {
                        // Display an error message for invalid format
                        Console.WriteLine($"Invalid format in line {lineCount} of {filePath}");
                    }
                    lineCount++; // Increment the line count for the next line
                }
            }
        }
        else
        {
            // Error message if the file is not found
            Console.WriteLine($"File not found: {filePath}");
        }
    }
}

// Create a class for parsing pipe-delimited files
public class PipeFileParser : IFileParser
{
    public void ParseFile(string filePath)
    {
        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Initialize a line count for error reporting
            int lineCount = 1;

            // Read the file line by line until the end is reached
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] fields = line.Split('|'); // Split the line by pipe character

                    // Check if there are at least 7 fields
                    if (fields.Length >= 7)
                    {
                        // Get the directory where the source file is located
                        string sourceDirectory = Path.GetDirectoryName(filePath);

                        // Create the output file path in the same directory
                        string outputFilePath = Path.Combine(sourceDirectory, Path.GetFileNameWithoutExtension(filePath) + "_out.txt");

                        // Create an output file for writing
                        using (StreamWriter writer = new StreamWriter(outputFilePath, true))
                        {
                            // Write the parsed data to the output file
                            writer.WriteLine($"Line#{lineCount} :Field#1={fields[0]} ==> Field#2={fields[1]} ==> Field#3={fields[2]} ==> Field#4={fields[3]} ==> Field#5={fields[4]} ==> Field#6={fields[5]} ==> Field#7={fields[6]}");
                        }

                        // Success message
                        Console.WriteLine($"Processed {Path.GetFileName(filePath)} successfully.");
                    }
                    else
                    {
                        // Display an error message for invalid format
                        Console.WriteLine($"Invalid format in line {lineCount} of {filePath}");
                    }
                    lineCount++; // Increment the line count for the next line
                }
            }
        }
        else
        {
            // Error message if the file is not found
            Console.WriteLine($"File not found: {filePath}");
        }
    }
}

// Create a class for processing the files
public class FileParserEngine
{
    public void ProcessFiles(List<string> filePaths)
    {
        foreach (var filePath in filePaths)
        {
            if (File.Exists(filePath))
            {
                string fileExtension = Path.GetExtension(filePath).ToLower();

                if (fileExtension == ".csv")
                {
                    IFileParser parser = new CsvFileParser(); // Use the CSV parser
                    parser.ParseFile(filePath);
                }
                else if (fileExtension == ".txt")
                {
                    IFileParser parser = new PipeFileParser(); // Use the pipe-delimited parser
                    parser.ParseFile(filePath);
                }
                else
                {
                    Console.WriteLine($"Unsupported file type: {fileExtension}");
                }
            }
            else
            {
                Console.WriteLine($"File not found: {filePath}");
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<string> filesToProcess = new List<string>
        {
            "SampleCSV.csv",
            "SamplePipe.txt"
        };

        var parserEngine = new FileParserEngine();
        parserEngine.ProcessFiles(filesToProcess);
    }
}
