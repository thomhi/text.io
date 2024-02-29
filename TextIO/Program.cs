// ----------------------------------------- BUILDER -----------------------------------------------------

using DataManager;
using DataManager.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();
builder.Services.AddDbContextFactory<TextDbContext>();
var app = builder.Build();
// ----------------------------------------- APPLICATION -----------------------------------------------------

app.MapPost("/create-room/{name}", (string name) => Console.WriteLine($"Creating of Room {name}"));
app.MapPost("/add-word-to-wordlist/{word}", (string word, TextDbContext ctx) =>
{
    ctx.Words.Add(new Words { Word = word });
    ctx.SaveChanges();
});

app.MapGet("/get-random-word", (TextDbContext ctx) =>
{
    var amount = ctx.Words.Count();
    var randomId = Random.Shared.Next(1, amount + 1);
    return ctx.Words.Find(randomId);
});



app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.Run();
