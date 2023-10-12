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
            // Open the file for reading
            using (StreamReader reader = new StreamReader(filePath))
            {
                int lineCount = 1; // Initialize a line count for error reporting

                // Read the file line by line until the end is reached
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine(); // Read a line from the file
                    string[] fields = line.Split(','); // Split the line by commas (CSV)

                    // Check if there are at least 6 fields
                    if (fields.Length >= 6)
                    {
                        // create an output file for writing
                        using (StreamWriter writer = new StreamWriter(@"C:\Users\markg\source\repos\TextFileAssigment\" + Path.GetFileNameWithoutExtension(filePath) + "_out.txt", true))
                        {
                            // Write the parsed data to the output file
                            writer.WriteLine($"Line#{lineCount} :Field#1={fields[0]} ==> Field#2={fields[1]} ==> Field#3={fields[2]} ==> Field#4={fields[3]} ==> Field#5={fields[4]} ==> Field#6={fields[5]}");
                        }

                        // success message
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
            //  error message if file not found
            Console.WriteLine($"File not found: {filePath}");
        }
    }
}

//  class for parsing pipe-delimited files
public class PipeFileParser : IFileParser
{
    public void ParseFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                int lineCount = 1;
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] fields = line.Split('|'); // Split the line by pipe character

                    if (fields.Length >= 7)
                    {
                        using (StreamWriter writer = new StreamWriter(@"C:\Users\markg\source\repos\TextFileAssigment\" + Path.GetFileNameWithoutExtension(filePath) + "_out.txt", true))
                        {
                            writer.WriteLine($"Line#{lineCount} :Field#1={fields[0]} ==> Field#2={fields[1]} ==> Field#3={fields[2]} ==> Field#4={fields[3]} ==> Field#5={fields[4]} ==> Field#6={fields[5]} ==> Field#7={fields[6]}");
                        }

                        Console.WriteLine($"Processed {Path.GetFileName(filePath)} successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Invalid format in line {lineCount} of {filePath}");
                    }
                    lineCount++;
                }
            }
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }
    }
}

//  class for processing the files
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
            @"C:\Users\markg\source\repos\TextFileAssigment\SampleCSV.csv",
            @"C:\Users\markg\source\repos\TextFileAssigment\SamplePipe.txt"
        };

        var parserEngine = new FileParserEngine();
        parserEngine.ProcessFiles(filesToProcess);
    }
}
