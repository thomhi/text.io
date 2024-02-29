// ----------------------------------------- BUILDER -----------------------------------------------------

using DataManager;
using TextIO;
using TextIO.Events;
using TextIO.ReadData;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();
builder.Services.AddDbContextFactory<TextDbContext>();
builder.Services.AddSingleton<EventStore>();
builder.Services.AddSingleton<Statistics>();
builder.Services.AddHostedService<Projection>();
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
