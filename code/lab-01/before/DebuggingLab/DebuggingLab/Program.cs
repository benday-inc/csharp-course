using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DebuggingLab;

    
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Simulating user - specific data processing...");

        var tasks = new[]
        {
            Task.Run(() => SimulateUserProcessing("User1", new[] { "ItemA", "ItemB" })),
            Task.Run(() => SimulateUserProcessing("User2", new[] { "ItemX", "ItemY" }))
        };
        Task.WaitAll(tasks);
        Console.WriteLine("Simulation complete.");
    }

    static void SimulateUserProcessing(string userId, string[] items)
    {
        Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] Starting processing for { userId}");

        var processor = new DataProcessor(userId);
        processor.ProcessData(items);

        Thread.Sleep(new Random().Next(10, 100));

        var processedItems = processor.GetProcessedData();
        Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] { userId}'s Processed Data: { string.Join(", ", processedItems)}");
    }
}
