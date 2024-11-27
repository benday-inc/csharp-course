using System;
using System.Linq;

namespace DebuggingLab;

    
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Simulating user - specific data processing...");

        var tasks = new[]
        {
            Task.Run(() => SimulateUserProcessing("User1", new[] { "ItemA", "ItemB", "ItemC", "ItemD" })),
            Task.Run(() => SimulateUserProcessing("User2", new[] { "ItemX", "ItemY", "ItemZ", "Item1234" })),
            Task.Run(() => SimulateUserProcessing("User3", new[] { "ItemM", "ItemN", "ItemO", "ItemP" })),
            Task.Run(() => SimulateUserProcessing("User4", new[] { "ItemE", "ItemF", "ItemG", "ItemH" })),
            Task.Run(() => SimulateUserProcessing("User5", new[] { "ItemE", "ItemF", "ItemG", "ItemH" })),
            Task.Run(() => SimulateUserProcessing("User6", new[] { "ItemX", "ItemY", "ItemX1", "ItemY2" })),
            Task.Run(() => SimulateUserProcessing("User7", new[] { "ItemL", "ItemU", "ItemZxcxzcv" })),
        };
        Task.WaitAll(tasks);
        Console.WriteLine("Simulation complete.");
    }

    static void SimulateUserProcessing(string userId, string[] items)
    {
        Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] Starting processing for { userId}");

        var processor = new DataProcessor(userId);
        processor.ProcessData(items);

        Thread.Sleep(new Random().Next(100, 1000));

        var processedItems = processor.GetProcessedData();
        Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] { userId}'s Processed Data: { string.Join(", ", processedItems)}");
    }
}
