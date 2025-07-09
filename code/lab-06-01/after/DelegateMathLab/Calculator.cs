namespace DelegateMathLab
{
    public class Calculator
    {
        public double Compute(double a, double b, MathOperation op, Logger? log = null)
        {
            double result = op(a, b);
            log?.Invoke($"Computed {a} and {b}: Result = {result}");
            return result;
        }
    }
}