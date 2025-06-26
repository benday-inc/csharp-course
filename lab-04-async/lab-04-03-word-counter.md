# Hands-On Lab: Using `ConcurrentDictionary` in C#

## Overview

In this lab, you'll learn how to use the `ConcurrentDictionary` from the `System.Collections.Concurrent` namespace. You will create a word frequency counter that processes multiple text files concurrently and stores the results in a thread-safe dictionary.

---

## Prerequisites

- Basic understanding of C# and .NET.
- Familiarity with `Task`, `async`, and `await`.
- Visual Studio or a similar IDE.

---

## Objectives

By the end of this lab, you will:
1. Understand the purpose and usage of `ConcurrentDictionary`.
2. Learn to safely update shared data structures in a multithreaded environment.
3. Build a word frequency counter that processes text files concurrently.

---

## Step 1: Load the Project

1. Open Visual Studio
2. Load the project `WordCounter-before.sln` in the `lab-04-03-word-count` folder.


---

## Step 2: Create the Word Counter

1. In the `WordCounter.ConsoleUi` project, add the following code to `Program.cs`:

   ```csharp
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
   
           await Task.WhenAll(tasks);
   
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
   ```

---

## Step 4: Test the Application

1. Run the program.
2. Observe the output displaying the word frequencies from all the text files.

---

## Step 5: Enhance the Word Counter

1. Add support for case-insensitive word counting (already implemented using `StringComparer.OrdinalIgnoreCase` in the `ConcurrentDictionary`).
2. Extend the program to handle large files:
   - Use `File.ReadLines` instead of `File.ReadAllText` for memory efficiency.

   ```csharp
   static void ProcessFile(string filePath, ConcurrentDictionary<string, int> wordCounts)
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
   ```

---

## Step 6: Handle Exceptions

1. Add error handling to ensure robustness:

   ```csharp
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
   ```

---

## Step 7: Make it Single-Threaded

Let's see how much performance boost we get from being multi-threaded. 

* Run the app using **CTRL-F5** (no debugging) and make a note of how long it takes to run.

Next let's modify the file to make it run single-threaded.

* Find the `await Task.WhenAll(tasks)` call
* Comment that line out and replace it with a standard `foreach`

```csharp
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
```



* Run the application using **CTRL-F5** and compare the run time

How did it do?

## Summary

In this lab, you:
1. Used a `ConcurrentDictionary` to count word frequencies across multiple files concurrently.
2. Leveraged `AddOrUpdate` to safely update the dictionary in a multithreaded environment.
3. Extended the program to handle large files and errors gracefully.
