using System;
using System.Collections.Concurrent;
using System.Linq;

namespace DebuggingLab;

class DataProcessor
{
    private readonly string _userId;
    private static readonly ConcurrentDictionary<string, ProcessedData> _cache = new();
    private static ProcessedData? _userData;
    private static readonly object _lock = new();

    public DataProcessor(string userId)
    {
        _userId = userId;
    }

    public void ProcessData(string[] data)
    {
        PopulateDataCacheForUser(_userId);

        if (_userData == null)
        {
            return;
        }
        else
        {
            EnsureDataCacheIsSaved(_userId, _userData);

            foreach (var item in data)
            {
                Thread.Sleep(new Random().Next(100, 500));

                _userData.Data.Add($"{_userId}-{item}");
            }
        }

    }

    private static void PopulateDataCacheForUser(string userId)
    {
        lock (_lock)
        {
            if (_userData == null)
            {
                _cache.Clear();
                _userData = new ProcessedData()
                {
                    Username = userId
                };
            }
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
