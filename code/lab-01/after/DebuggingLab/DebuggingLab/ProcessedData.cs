using System;
using System.Linq;

namespace DebuggingLab;

public class ProcessedData
{
    public string Username { get; set; } = string.Empty;
    public List<string> Data { get; set; } = new();
}