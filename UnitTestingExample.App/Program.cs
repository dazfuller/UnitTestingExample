using System;

namespace UnitTestingExample.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var generator = new IpsumDataGenerator();
            var dummyText = generator.Generate();

            Console.WriteLine(dummyText);
        }
    }
}
