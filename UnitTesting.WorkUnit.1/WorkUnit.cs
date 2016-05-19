namespace UnitTesting.WorkUnit._1
{
    public class WorkUnit
    {
        private readonly ILogger _logger;

        public WorkUnit()
        {
            _logger = new SimpleLogger();
        }

        public int GetNthFibonacci(int n)
        {
            _logger.Log("Starting Fibonacci calculation");

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
