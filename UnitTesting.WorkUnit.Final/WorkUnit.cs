using System;

namespace UnitTesting.WorkUnit.Final
{
    public class WorkUnit
    {
        private readonly ILogger _logger;

        public WorkUnit(ILogger logger)
        {
            _logger = logger;
        }

        public int GetNthFibonacci(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("Must be a positive integer", nameof(n));
            }

            _logger.Log("Starting Fibonacci calculation");

            if (n == 0)
            {
                _logger.Log("No calculation required");
                return 0;
            }

            if (n == 1)
            {
                _logger.Log("No calculation required");
                return 1;
            }

            var a = 0;
            var b = 1;
            var c = 0;

            for (var i = 2; i <= n; i++)
            {
                c = a + b;
                a = b;
                b = c;
            }

            _logger.Log($"Finished calculation for n={n}: {c}");

            return c;
        }
    }
}
