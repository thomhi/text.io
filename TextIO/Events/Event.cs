namespace TextIO.Events;

public record Event<T> : EventBase
{
    public string Type { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Subject { get; set; }
    public T Data { get; set; }
}
