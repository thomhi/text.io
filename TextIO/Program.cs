// ----------------------------------------- BUILER -----------------------------------------------------
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();
var app = builder.Build();
// ----------------------------------------- APPLICATION -----------------------------------------------------

app.MapGet("/", () => "Hello World!");

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.Run();
