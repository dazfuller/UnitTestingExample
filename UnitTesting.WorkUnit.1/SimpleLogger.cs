using System;

namespace UnitTesting.WorkUnit._1
{
    public class SimpleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"Log: {message}");
        }
    }
}