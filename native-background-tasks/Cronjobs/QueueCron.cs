
using NativeBackgroundTasks.Service;

namespace NativeBackgroundTasks.Cronjobs
{
    public class QueueCron(IQueue queue, ILogger<QueueCron> logger) : BackgroundService
    {
        private readonly IQueue _queue = queue;
        private readonly ILogger<QueueCron> _logger = logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Ejecutando [QueueCron] a las: {time}", DateTimeOffset.Now);

            using PeriodicTimer timer = new(TimeSpan.FromSeconds(10));

            while (await timer.WaitForNextTickAsync(stoppingToken))
            { 
                var message = await _queue.PullFromQueueAsync(stoppingToken);
                _logger.LogInformation($"Message received from queue ---> {message}");
            }
        }
    }
}
