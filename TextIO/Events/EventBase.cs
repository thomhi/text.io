namespace TextIO.Events;

public record EventBase
{
    public string Type { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Subject { get; set; }
}
