using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataManager;
public class TextDbContext : 
    DbContext
{
    private readonly string _connectionString;
    public TextDbContext(IConfiguration config)
    {
        var relativePath = config.GetConnectionString("LotteryDraws") ?? throw new NullReferenceException();
        _connectionString = Path.GetFullPath(relativePath);
    }

    public DbSet<Words> Words { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_connectionString}");
    }
}
