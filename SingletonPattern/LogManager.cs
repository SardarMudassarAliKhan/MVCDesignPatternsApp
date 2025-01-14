namespace MVCDesignPatternsApp.SingletonPattern
{
    public sealed class LogManager
    {
        private static readonly Lazy<LogManager> instance = new(() => new LogManager());

        private LogManager() { }

        public static LogManager Instance => instance.Value;

        public void Log(string message) => Console.WriteLine($"Log: {message}");
    }
}
