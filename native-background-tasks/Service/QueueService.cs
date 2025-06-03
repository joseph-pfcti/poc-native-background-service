using System.Threading.Channels;

namespace NativeBackgroundTasks.Service
{
    public class QueueService : IQueue
    {
        private readonly Channel<string> _queue;
        private readonly ILogger<QueueService> _logger;

        public QueueService(ILogger<QueueService> logger)
        { 
            _logger = logger;
            var opts = new BoundedChannelOptions(10)
            {
                FullMode = BoundedChannelFullMode.Wait,
            };

            _queue = Channel.CreateBounded<string>(opts);
        }

        public async ValueTask<string> PullFromQueueAsync(CancellationToken cancellationToken)
        {
            var message = await _queue.Reader.ReadAsync(cancellationToken);
            _logger.LogInformation($"Reading message comming from --> {message}");
            return message;
        }

        public ValueTask<List<string>> PullAllFromQueueAsync(CancellationToken cancellationToken)
        {
            List<string> messages = [];

            while (_queue.Reader.TryRead(out var message))
            {
                messages.Add(message);
            }

            return ValueTask.FromResult(messages);
        }

        public async ValueTask PushToQueueAsync(string message)
        {
            await _queue.Writer.WriteAsync(message);
            _logger.LogInformation("Message added to Queue message");
        }
    }
}
