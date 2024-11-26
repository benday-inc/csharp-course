using System;
using System.Collections.Concurrent;
using System.Linq;

namespace DebuggingLab;

class DataProcessor
{
    private readonly string _userId;
    private static readonly Dictionary<string, List<string>> _cache = new();
    private static List<string>? _currentCache;

    public DataProcessor(string userId)
    {
        _userId = userId;
    }

    public void ProcessData(string[] data)
    {
        if (_currentCache == null)
        {
            _currentCache = new List<string>();
        }
                
        _cache.Add(_userId, _currentCache);

        foreach (var item in data)
        {
            Thread.Sleep(new Random().Next(100, 500));
            _currentCache.Add($"{_userId}-{item}");
        }
    }

    public string[] GetProcessedData()
    {
        if (_cache.TryGetValue(_userId, out var processedData))
        {
            return processedData.ToArray();
        }
        Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Warning: No data found for {_userId}. Returning empty array.");
        return Array.Empty<string>();
    }
}