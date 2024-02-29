namespace TextIO.Events;

public record Event<T> : EventBase
{
    public T Data { get; set; }
}
