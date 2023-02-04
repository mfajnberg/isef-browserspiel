using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Diagnostics.Eventing.Reader;

namespace web_api
{
    /// <summary>
    /// a custom Console Logger 
    /// </summary>
    public static class ConsoleLogger
    {
        static ConsoleColor defaultBackgroundColor = Console.BackgroundColor;
        static ConsoleColor defaultForegroundColor = Console.ForegroundColor;

        /// <summary>
        /// Logs the given message in INFO-Level
        /// </summary>
        /// <param name="message">the message to log</param>
        public static void LogInfo(string message)
        {
            SaveConsoleColors();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" INFO: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);

            RestoreConsoleColors();
        }

        /// <summary>
        /// Logs the given message in WARNING-Level
        /// </summary>
        /// <param name="message">the message to log</param>
        public static void LogWarning(string message)
        {
            SaveConsoleColors();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" WARNING: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);

            RestoreConsoleColors();
        }

        /// <summary>
        /// Logs the given message in ERROR-Level
        /// </summary>
        /// <param name="message">the message to log</param>
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

        /// <summary>
        /// writes a line with the given character
        /// </summary>
        /// <param name="v"></param>
        /// <param name="backColor"></param>
        /// <param name="foreColor"></param>
        private static void WriteDash(string v, ConsoleColor backColor, ConsoleColor foreColor)
        {
            Console.BackgroundColor = backColor;
            Console.ForegroundColor = foreColor;

            for (int i = 0; i < Console.BufferWidth; i++)
                Console.Write(v);

            Console.WriteLine();

            RestoreConsoleColors();
        }

        /// <summary>
        /// saves the default console colors
        /// </summary>
        private static void SaveConsoleColors()
        {
            defaultBackgroundColor = Console.BackgroundColor;
            defaultForegroundColor = Console.ForegroundColor;
        }

        /// <summary>
        /// restores the console colors to the default colors 
        /// </summary>
        private static void RestoreConsoleColors()
        {
            Console.BackgroundColor = defaultBackgroundColor;
            Console.ForegroundColor = defaultForegroundColor;
        }
    }
}
