using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    public static async Task Main(string[] args)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        Console.WriteLine("Starting word count application...");

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

        var runMultithreaded = false;

        if (runMultithreaded == true)
        {
            await Task.WhenAll(tasks);
        }
        else
        {   
            foreach (var task in tasks)
            {
                await task;
            }
        }        

        stopwatch.Stop();
        
        Console.WriteLine("Word count completed. Results:");

        var totalWords = wordCounts.Count;
        Console.WriteLine($"Total unique words counted: {totalWords}");
        if (totalWords == 0)
        {
            Console.WriteLine("No words were counted.");
            return;
        }

        // most popular word
        var mostPopularWord = wordCounts.OrderByDescending(kv => kv.Value).FirstOrDefault();

        Console.WriteLine($"Most popular word: '{mostPopularWord.Key}' with {mostPopularWord.Value} occurrences.");

        // top 20 words

        var top20Words = wordCounts.OrderByDescending(kv => kv.Value).Take(20);
        Console.WriteLine("Top 20 words:");
        foreach (var kv in top20Words)
        {
            Console.WriteLine($"'{kv.Key}': {kv.Value}");
        }

        Console.WriteLine($"Word count completed in {stopwatch.ElapsedMilliseconds} ms.");
    }

    static void ProcessFile(string filePath, ConcurrentDictionary<string, int> wordCounts)
    {
        try
        {
            Console.WriteLine($"Processing {Path.GetFileName(filePath)}...");

            foreach (var line in File.ReadLines(filePath))
            {
                var words = line.Split(new[] { ' ', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var word in words)
                {
                    var wordToLower = word.ToLower();
                    wordCounts.AddOrUpdate(
                        wordToLower,          // Key
                        1,             // Value if the key does not exist
                        (_, count) => count + 1 // Update logic if the key exists
                    );
                }
            }

            Console.WriteLine($"Finished processing {Path.GetFileName(filePath)}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing {filePath}: {ex.Message}");
        }
    }
}