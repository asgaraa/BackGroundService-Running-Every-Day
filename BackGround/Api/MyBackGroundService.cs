namespace Api
{
    public class MyBackGroundService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILogger<WeatherForecast> _logger;

        public MyBackGroundService(ILogger<WeatherForecast> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var weatherTime = new DateTime(now.Year, now.Month, now.Day, 09, 46, 0); // 09:45

            if (now > weatherTime)
            {
                weatherTime = weatherTime.AddDays(1); // every day 09:45
            }

            var timeUntilFirstExecution = weatherTime - now;
            _timer = new Timer(DoWork, null, timeUntilFirstExecution, TimeSpan.FromDays(1));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("MyBackGroundService is running at: " + DateTime.Now);

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("MyBackGroundService is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
