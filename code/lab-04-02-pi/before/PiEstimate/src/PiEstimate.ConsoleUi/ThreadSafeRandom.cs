partial class Program
{
    public static class ThreadSafeRandom
    {
        private static int _seed = Environment.TickCount;
        private static readonly ThreadLocal<Random> _random =
            new(() => new Random(Interlocked.Increment(ref _seed)));

        public static Random ThisThreadsRandom => _random.Value!;
    }
}