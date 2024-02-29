namespace TextIO.Events;

public record CreateRoomEvent
{
    public string RoomName { get; set; }
}
