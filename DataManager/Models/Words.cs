using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Models;
public class Words
{
    [Key]
    public int Id { get; set; }
    public string Word { get; set; } = null!;
}
