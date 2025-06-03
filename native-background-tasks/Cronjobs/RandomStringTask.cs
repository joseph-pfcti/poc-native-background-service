
using NativeBackgroundTasks.Service;
using Cronos;

namespace NativeBackgroundTasks.Cronjobs
{
    public class RandomStringTask(RandomStringService randomStringService, ILogger<RandomStringTask> logger) : BackgroundService
    {
        private readonly RandomStringService _randomStringService = randomStringService;
        private readonly CronExpression _cronExpression = CronExpression.Parse("* * * * *");
        private readonly ILogger<RandomStringTask> _logger = logger;


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Ejecutando tarea programada a las: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested) 
            {
                var next = _cronExpression.GetNextOccurrence(DateTimeOffset.UtcNow, TimeZoneInfo.Local);

                if (next.HasValue)
                {
                    var delay = next.Value - DateTimeOffset.Now;
                    if (delay.TotalMilliseconds > 0)
                        await Task.Delay(delay, stoppingToken);

                    _logger.LogInformation("Ejecutando llamado a las: {time}", DateTimeOffset.Now);
                    await _randomStringService.GetRandomStringAsync(stoppingToken);
                }
            }
        }
    }
}
