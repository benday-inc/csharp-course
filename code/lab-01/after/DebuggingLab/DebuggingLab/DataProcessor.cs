using System;
using System.Collections.Concurrent;
using System.Linq;

namespace DebuggingLab;

class DataProcessor
{
    private readonly string _userId;
    private static readonly ConcurrentDictionary<string, ProcessedData> _cache = new();
    private ProcessedData _userData;
    private static readonly object _lock = new();

    public DataProcessor(string userId)
    {
        _userId = userId;
        _userData = new();
        EnsureDataCacheIsSaved(_userId, _userData);
    }

    public void ProcessData(string[] data)
    {
        foreach (var item in data)
        {
            Thread.Sleep(new Random().Next(100, 500));

            _userData.Data.Add($"{_userId}-{item}");
        }
    }

    private static void EnsureDataCacheIsSaved(string userId, ProcessedData data)
    {
        if (_cache.ContainsKey(userId) == false)
        {
            _cache.TryAdd(userId, data);
        }
    }

    public string[] GetProcessedData()
    {
        lock (_lock)
        {
            if (_cache.TryGetValue(_userId, out var processedData))
            {
                return processedData.Data.ToArray();
            }
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Warning: No data found for {_userId}. Returning empty array.");
            return Array.Empty<string>();
        }
    }
}
