using DataManager;
using TextIO.ReadData;

namespace TextIO;

public static class ApiQuery
{
    public static WebApplication AddQueryApi(this WebApplication app)
    {
        app.MapGet("/get-random-word", (TextDbContext ctx) =>
        {
            var amount = ctx.Words.Count();
            var randomId = Random.Shared.Next(1, amount + 1);
            return ctx.Words.Find(randomId);
        });

        app.MapGet("/event-statistics", (Statistics sta) => TypedResults.Ok(sta.EventCounts));
        return app;
    }
}
