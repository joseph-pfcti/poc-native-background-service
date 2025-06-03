namespace NativeBackgroundTasks.Service
{
    public interface IQueue
    {
        ValueTask PushToQueueAsync(string message);
        ValueTask<string> PullFromQueueAsync(CancellationToken cancellationToken);
        ValueTask<List<string>> PullAllFromQueueAsync(CancellationToken cancellationToken);
    }
}
