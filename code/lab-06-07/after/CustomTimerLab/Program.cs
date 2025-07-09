using System;

class Program
{
    static void Main()
    {
        var timer = new CustomTimer(1000);
        var logger = new Logger();
        var uiUpdater = new UIUpdater();

        timer.Tick += logger.LogTime;
        timer.Tick += uiUpdater.UpdateUI;

        timer.Start();

        Console.WriteLine("Press Enter to unsubscribe Logger...");
        Console.ReadLine();

        timer.Tick -= logger.LogTime;

        Console.WriteLine("Press Enter to stop the timer...");
        Console.ReadLine();

        timer.Stop();
    }
}