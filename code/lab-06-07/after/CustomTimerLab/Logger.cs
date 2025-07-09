using System;

public class Logger
{
    public void LogTime(DateTime currentTime)
    {
        Console.WriteLine($"[Logger] Tick at: {currentTime}");
    }
}