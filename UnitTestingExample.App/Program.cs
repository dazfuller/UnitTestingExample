using System;

namespace UnitTestingExample.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new SimpleLogger();
            var workUnit = new WorkUnit(logger);

            Console.WriteLine(workUnit.GetNthFibonacci(10));
        }
    }
}
