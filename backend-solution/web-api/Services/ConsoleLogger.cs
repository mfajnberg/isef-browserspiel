using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Diagnostics.Eventing.Reader;

namespace web_api
{
    public static class ConsoleLogger
    {
        static ConsoleColor defaultBackgroundColor = Console.BackgroundColor;
        static ConsoleColor defaultForegroundColor = Console.ForegroundColor;

        public static void LogInfo(string message)
        {
            SaveConsoleColors();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" INFO: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);

            RestoreConsoleColors();
        }

        public static void LogWarning(string message)
        {
            SaveConsoleColors();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" WARNING: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);

            RestoreConsoleColors();
        }
        public static void LogError(string message)
        {
            SaveConsoleColors();

            Console.WriteLine();

            WriteDash("*", ConsoleColor.Red, ConsoleColor.Gray);

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(" ERROR: ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(message);

            WriteDash("*", ConsoleColor.Red, ConsoleColor.Gray);

            RestoreConsoleColors();

            Console.WriteLine();
        }

        private static void WriteDash(string v, ConsoleColor backColor, ConsoleColor foreColor)
        {
            Console.BackgroundColor = backColor;
            Console.ForegroundColor = foreColor;

            for (int i = 0; i < Console.BufferWidth; i++)
                Console.Write(v);

            Console.WriteLine();

            RestoreConsoleColors();
        }

        private static void SaveConsoleColors()
        {
            defaultBackgroundColor = Console.BackgroundColor;
            defaultForegroundColor = Console.ForegroundColor;
        }

        private static void RestoreConsoleColors()
        {
            Console.BackgroundColor = defaultBackgroundColor;
            Console.ForegroundColor = defaultForegroundColor;
        }
    }

    //public sealed class ConsoleLogger : ILogger
    //{
    //    private readonly string _name;
    //    private readonly Func<ConsoleLoggerConfiguration> _getCurrentConfig;

    //    public ConsoleLogger(string name,
    //    Func<ConsoleLoggerConfiguration> getCurrentConfig) =>
    //        (_name, _getCurrentConfig) = (name, getCurrentConfig);

    //    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

    //    public bool IsEnabled(LogLevel logLevel) => _getCurrentConfig().LogLevelToColorMap.ContainsKey(logLevel);

    //    void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    //    {
    //        if (!IsEnabled(logLevel))
    //            return;

    //        ConsoleLoggerConfiguration config = _getCurrentConfig();
    //        if (config.EventId == 0 || config.EventId == eventId.Id)
    //        {
    //            ConsoleColor originalColor = Console.ForegroundColor;

    //            Console.ForegroundColor = config.LogLevelToColorMap[logLevel];
    //            Console.WriteLine($"[{eventId.Id,2}: {logLevel,-12}]");

    //            Console.ForegroundColor = originalColor;
    //            Console.Write($"     {_name} - ");

    //            Console.ForegroundColor = config.LogLevelToColorMap[logLevel];
    //            Console.Write($"{formatter(state, exception)}");

    //            Console.ForegroundColor = originalColor;
    //            Console.WriteLine();
    //        }
    //    }
    //}

    //public sealed class ConsoleLoggerConfiguration
    //{
    //    public int EventId { get; set; }

    //    public Dictionary<LogLevel, ConsoleColor> LogLevelToColorMap { get; set; } = new()
    //    {
    //        [LogLevel.Information] = ConsoleColor.Green
    //    };
    //}

    //[ProviderAlias("Console")]
    //public sealed class ConsoleLoggerProvider : ILoggerProvider
    //{
    //    private readonly IDisposable? _onChangeToken;
    //    private ConsoleLoggerConfiguration _currentConfig;
    //    private readonly ConcurrentDictionary<string, ConsoleLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

    //    public ConsoleLoggerProvider(IOptionsMonitor<ConsoleLoggerConfiguration> config)
    //    {
    //        _currentConfig = config.CurrentValue;
    //        _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
    //    }
    //    public ILogger CreateLogger(string categoryName) =>
    //    _loggers.GetOrAdd(categoryName, name => new ConsoleLogger(name, GetCurrentConfig));

    //    private ConsoleLoggerConfiguration GetCurrentConfig() => _currentConfig;

    //    public void Dispose()
    //    {
    //        _loggers.Clear();
    //        _onChangeToken?.Dispose();
    //    }
    //}
}
