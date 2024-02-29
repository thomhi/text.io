// ----------------------------------------- BUILDER -----------------------------------------------------

using DataManager;
using TextIO;
using TextIO.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();
builder.Services.AddDbContextFactory<TextDbContext>();
builder.Services.AddSingleton<EventStore>();
var app = builder.Build();
// ----------------------------------------- APPLICATION -----------------------------------------------------

app.AddCommandApi();
app.AddQueryApi();
app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.Run();
