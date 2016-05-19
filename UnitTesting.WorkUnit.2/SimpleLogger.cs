using System;

namespace UnitTesting.WorkUnit._2
{
    public class SimpleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"Log: {message}");
        }
    }
}