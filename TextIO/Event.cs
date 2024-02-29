namespace TextIO;

public class Event<T>
{
    public string Type { get; set; }
    public DateTime TimeStamp { get; }
    public string Subject { get; set; }
    public T Data { get; set; }

    Event(T data, string type, string subject)
    {
        Type = type;
        TimeStamp = DateTime.Now;
        Subject = subject;
        Data = data;
    }
}
