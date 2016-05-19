using System;

namespace UnitTestingExample
{
    public class SimpleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"Log: {message}");
        }
    }
}