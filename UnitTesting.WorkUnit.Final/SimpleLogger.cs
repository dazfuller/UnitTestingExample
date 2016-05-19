using System;

namespace UnitTesting.WorkUnit.Final
{
    public class SimpleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"Log: {message}");
        }
    }
}