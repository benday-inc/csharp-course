using System;

namespace DelegateMathLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var calc = new Calculator();

            // Basic operations using method group syntax
            Console.WriteLine($"Add: {calc.Compute(4, 2, Add)}");
            Console.WriteLine($"Subtract: {calc.Compute(4, 2, Subtract)}");
            Console.WriteLine($"Multiply: {calc.Compute(4, 2, Multiply)}");
            Console.WriteLine($"Divide: {calc.Compute(4, 2, Divide)}");

            // Using lambda expressions
            Console.WriteLine($"Power: {calc.Compute(2, 3, (x, y) => Math.Pow(x, y))}");
            Console.WriteLine($"Modulus: {calc.Compute(10, 3, (x, y) => x % y)}");

            // Logging
            Logger consoleLogger = msg => Console.WriteLine($"[LOG] {msg}");
            var loggedResult = calc.Compute(5, 3, Add, consoleLogger);
            Console.WriteLine($"Logged Result: {loggedResult}");
        }

        static double Add(double x, double y) => x + y;
        static double Subtract(double x, double y) => x - y;
        static double Multiply(double x, double y) => x * y;
        static double Divide(double x, double y) => y != 0 ? x / y : double.NaN;
    }
}