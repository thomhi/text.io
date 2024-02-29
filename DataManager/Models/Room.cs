using System.ComponentModel.DataAnnotations;

namespace DataManager.Models;
public class Room
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? CurrentWordToGuess {get; set; } = null!;
}
