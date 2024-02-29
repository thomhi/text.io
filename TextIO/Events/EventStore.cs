namespace TextIO.Events;

public sealed class EventStore
{
    public List<EventBase> Events { get; } = [];

    public void StoreEvent(List<EventBase> events)
    {
        Events.AddRange(events);    
    }
}
