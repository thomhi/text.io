using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace TextIO.Events;

public sealed class EventStore
{
    public List<EventBase> Events { get; } = [];

    public IObservable<EventBase> EventsStream { get; set; } = new Subject<EventBase>();

    public void StoreEvent(List<EventBase> events)
    {
        Events.AddRange(events);
        foreach (var ev in events)
        {
            EventsStream.Publish(ev);
        }
    }

    public void StoreEvent(EventBase ev)
    {
        Events.Add(ev);
        EventsStream.Publish(ev);
    }
}
