using System;

namespace UnitTestingExample.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var workUnit = new WorkUnit();

            Console.WriteLine(workUnit.GetNthFibonacci(10));
        }
    }
}
