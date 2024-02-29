// ----------------------------------------- BUILDER -----------------------------------------------------

using DataManager;
using TextIO;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();
builder.Services.AddDbContextFactory<TextDbContext>();
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
