// Take a look at https://code-maze.com/aspnetcore-different-ways-to-run-background-tasks/
// Here services or interfaces are described with multiple scenarios

using NativeBackgroundTasks.Service;

namespace NativeBackgroundTasks.Cronjobs
{
    public class RandomStringCron(RandomStringService randomStringService, ILogger<RandomStringCron> logger) : BackgroundService
    {
        private readonly RandomStringService _randomStringService = randomStringService;
        private readonly ILogger<RandomStringCron> _logger = logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Ejecutando tarea programada a las: {time}", DateTimeOffset.Now);

            using PeriodicTimer timer = new(TimeSpan.FromMinutes(1));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken)) 
                {
                    _logger.LogInformation("Ejecutando llamado a las: {time}", DateTimeOffset.Now);
                    await _randomStringService.GetRandomStringAsync(stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Timed Hosted Service is stopping.");
            }

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    var next = _cronExpression.GetNextOccurrence(DateTimeOffset.UtcNow, TimeZoneInfo.Local);

            //    var delay = next.Value - DateTimeOffset.Now;
            //    if (delay.TotalMilliseconds > 0)
            //        await Task.Delay(delay, stoppingToken);

            //    _logger.LogInformation("Ejecutando llamado a las: {time}", DateTimeOffset.Now);
            //    await _randomStringService.GetRandomStringAsync(stoppingToken);
          
            //}
        }
    }
}
