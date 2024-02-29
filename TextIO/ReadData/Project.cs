using System.Reactive.Linq;
using TextIO.Events;

namespace TextIO.ReadData;

public sealed class Projection(Statistics statistics, EventStore eventStore) : IHostedService
{
    private IDisposable? _disposableSub;
    public void OnEvent(EventBase ev)
    {
        switch (ev.Type)
        {
            case nameof(RoomCreated):
                statistics.EventCounts.TryAddOrIncrement(nameof(RoomCreated));
                break;
            case nameof(WordGuessed):
                statistics.EventCounts.TryAddOrIncrement(nameof(WordGuessed));
                break;
            case nameof(WordDescribed):
                statistics.EventCounts.TryAddOrIncrement(nameof(WordDescribed));
                break;
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _disposableSub = eventStore.EventsStream.Subscribe(OnEvent);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _disposableSub?.Dispose();
        return Task.CompletedTask;
    }
}
