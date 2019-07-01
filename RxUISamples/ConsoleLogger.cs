using System;
using System.ComponentModel;
using Splat;
namespace RxUISamples
{
    public class ConsoleLogger : ILogger
    {


        public LogLevel Level { get; set; }

        public void Write([Localizable(false)] string message, LogLevel logLevel)
        {
            Console.WriteLine(message);
        }

        public void Write(Exception exception, [Localizable(false)] string message, LogLevel logLevel)
        {
            Console.WriteLine(message);
        }

        public void Write([Localizable(false)] string message, [Localizable(false)] Type type, LogLevel logLevel)
        {
            Console.WriteLine(message);
        }

        public void Write(Exception exception, [Localizable(false)] string message, [Localizable(false)] Type type, LogLevel logLevel)
        {
            Console.WriteLine(message);
        }
    }
}
