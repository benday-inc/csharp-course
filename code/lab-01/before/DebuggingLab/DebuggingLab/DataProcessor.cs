using System;
using System.Collections.Concurrent;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DebuggingLab;

class DataProcessor
{
    private readonly string _userId;
    private static readonly Lazy<ConcurrentDictionary<string, string[]>> _cache =
        new(() => new ConcurrentDictionary<string, string[]>());

    public DataProcessor(string userId)
    {
        _userId = userId;
    }

    public void ProcessData(string[] data)
    {
        _cache.Value[_userId] = data.Select(d => $"{_userId}-{d}").ToArray();
    }

    public string[] GetProcessedData()
    {
        if (_cache.Value.TryGetValue(_userId, out var processedData))
        {
            return processedData;
        }
        Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Warning: No data found for {_userId}. Returning empty array.");
        return Array.Empty<string>();
    }
}