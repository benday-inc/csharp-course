using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        // Default to 10 million samples unless overridden by first CLI arg
        int iterations = args.Length > 0 && int.TryParse(args[0], out var n)
                         ? n
                         : 10_000_000;

        Console.WriteLine($"Samples : {iterations:N0}");

        var stopwatch = Stopwatch.StartNew();
        var estimate = EstimatePiSequential(iterations);
        stopwatch.Stop();

        Console.WriteLine($"π ≈ {estimate:F6}");
        Console.WriteLine($"Elapsed                : {stopwatch.Elapsed.TotalSeconds:F2} s");
        Console.WriteLine($"Guess Error from Actual: {Math.Abs(Math.PI - estimate):F6}");
    }

    static double EstimatePiSequential(int iterations)
    {
        var rand = new Random();
        int inside = 0;

        for (int i = 0; i < iterations; i++)
        {
            double x = rand.NextDouble() * 2.0 - 1.0; // [-1, 1]
            double y = rand.NextDouble() * 2.0 - 1.0;

            if (x * x + y * y <= 1.0)
                inside++;
        }

        return (4.0 * inside) / iterations;
    }
}