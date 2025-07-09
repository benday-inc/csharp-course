using System;
using System.Timers;

public class CustomTimer
{
    private readonly System.Timers.Timer timer;

    public delegate void TickHandler(DateTime currentTime);
    public event TickHandler? Tick;

    public CustomTimer(double interval)
    {
        timer = new System.Timers.Timer(interval);
        timer.Elapsed += OnTimedEvent;
    }

    public void Start()
    {
        timer.Start();
        Console.WriteLine("Timer started.");
    }

    public void Stop()
    {
        timer.Stop();
        Console.WriteLine("Timer stopped.");
    }

    private void OnTimedEvent(object? sender, ElapsedEventArgs e)
    {
        Tick?.Invoke(DateTime.Now);
    }
}