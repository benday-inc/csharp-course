using System;
using System.Diagnostics;

partial class Program
{
    static void Main(string[] args)
    {
        // Default to 10 million samples
        int iterations = 10_000_000;
        Console.WriteLine($"Samples : {iterations:N0}");

        if (args.Contains("/multithreaded") == true)
        {
            var degree = GetDegreeOfParallelism(args, Environment.ProcessorCount);

            Console.WriteLine($"Processor Count: {Environment.ProcessorCount}");
            Console.WriteLine($"Parallelism: {degree} threads");

            var stopwatch = Stopwatch.StartNew();
            var estimate = EstimatePiParallel(iterations, degree);
            stopwatch.Stop();

            Console.WriteLine($"π ≈ {estimate:F6}");
            Console.WriteLine($"Elapsed                : {stopwatch.Elapsed.TotalSeconds:F2} s");
            Console.WriteLine($"Guess Error from Actual: {Math.Abs(Math.PI - estimate):F6}");
        }
        else
        {
            Console.WriteLine("Running in single-threaded mode.");

            var stopwatch = Stopwatch.StartNew();
            var estimate = EstimatePiSequential(iterations);
            stopwatch.Stop();

            Console.WriteLine($"π ≈ {estimate:F6}");
            Console.WriteLine($"Elapsed                : {stopwatch.Elapsed.TotalSeconds:F2} s");
            Console.WriteLine($"Guess Error from Actual: {Math.Abs(Math.PI - estimate):F6}");
        }
    }

    static int GetDegreeOfParallelism(string[] args, int defaultValue)
    {
        var arg = args.FirstOrDefault(a => a.StartsWith("/degree:", StringComparison.OrdinalIgnoreCase));
        if (arg != null && int.TryParse(arg.Split(':')[1], out int d) && d > 0)
            return d;
        return defaultValue;
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

    static double EstimatePiParallel(int iterations, int degree)
    {
        int insideGlobal = 0;

        var opts = new ParallelOptions
        {
            MaxDegreeOfParallelism = degree
        };

        // Parallel.For with thread-local counters
        Parallel.For(0, iterations, opts,
            () => 0,                               // thread-local init
            (i, state, localInside) =>             // loop body
            {
                var rand = ThreadSafeRandom.ThisThreadsRandom;
                double x = rand.NextDouble() * 2.0 - 1.0;
                double y = rand.NextDouble() * 2.0 - 1.0;
                if (x * x + y * y <= 1.0)
                    localInside++;
                return localInside;                // pass updated local value forward
            },
            localInside =>                         // local finally
                Interlocked.Add(ref insideGlobal, localInside));

        return 4.0 * insideGlobal / iterations;
    }
}