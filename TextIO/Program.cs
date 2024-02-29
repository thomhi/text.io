// ----------------------------------------- BUILDER -----------------------------------------------------
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();
var app = builder.Build();
// ----------------------------------------- APPLICATION -----------------------------------------------------

app.MapPost("/create-room/{name}", (string name) => Console.WriteLine($"Creating of Room {name}"));


app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.Run();
