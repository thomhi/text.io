using DataManager;
using DataManager.Models;
using Microsoft.EntityFrameworkCore;

namespace TextIO;

public static class ApiCommand
{
    public static WebApplication AddCommandApi(this WebApplication app)
    {
        app.MapPost("/create-room/{name}", (string name, TextDbContext ctx) =>
        {
            ctx.Rooms.Add(new Room { Name = name, CurrentWordToGuess = "test" });
            return TypedResults.Ok();
        }).AddEndpointFilter(async (invocationContext, next) =>
        {
            var name = invocationContext.GetArgument<string>(0);

            if (string.IsNullOrWhiteSpace(name))
            {
                return TypedResults.Problem("Null, Empty or only whitespace are not allowed.");
            }

            if (name.Length > 30)
            {
                return TypedResults.Problem("Name is too long. It is more then 30 chars.");
            }
            return await next(invocationContext);
        });

        app.MapPost("/add-word-to-wordlist/{word}", (string word, TextDbContext ctx) =>
        {
            ctx.Words.Add(new Words { Word = word });
            ctx.SaveChanges();
        });

        app.MapPost("/CleanUp", (TextDbContext ctx) =>
        {
            ctx.Database.ExecuteSqlRaw("DELETE FROM Words");
            ctx.Database.ExecuteSqlRaw("DELETE FROM Rooms");
        });
        return app;
    }
}
