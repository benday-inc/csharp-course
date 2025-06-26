using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var wordCounts = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        // get the dir for the assembly

        var assemblyDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ??
            throw new InvalidOperationException("Could not get executing assembly location");

        Console.WriteLine($"Executing assembly location: {assemblyDir}");
               
        var dataDir = Path.Combine(assemblyDir, "data");

        if (!Directory.Exists(dataDir))
        {
            Console.WriteLine($"Data directory '{dataDir}' does not exist.");
            return;
        }

        // Get all text files in the SampleTexts directory
        var textFiles = Directory.GetFiles(dataDir, "*.txt");

        if (textFiles.Length == 0)
        {
            Console.WriteLine("No text files found in the data directory.");
            return;
        }

        Console.WriteLine($"Found {textFiles.Length} text files in the data directory.");

        Console.WriteLine("Starting word count...");

        // Process files concurrently
        var tasks = textFiles.Select(file => Task.Run(() => ProcessFile(file, wordCounts)));

        await Task.WhenAll(tasks);

        Console.WriteLine("Word count completed. Results:");

        // Display results
        foreach (var kvp in wordCounts.OrderByDescending(kv => kv.Value))
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }

    static void ProcessFile(string filePath, ConcurrentDictionary<string, int> wordCounts)
    {
        Console.WriteLine($"Processing {Path.GetFileName(filePath)}...");

        var text = File.ReadAllText(filePath);
        var words = text.Split(new[] { ' ', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var word in words)
        {
            wordCounts.AddOrUpdate(
                word,          // Key
                1,             // Value if the key does not exist
                (_, count) => count + 1 // Update logic if the key exists
            );
        }

        Console.WriteLine($"Finished processing {Path.GetFileName(filePath)}.");
    }
}