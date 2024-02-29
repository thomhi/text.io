using DataManager;

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

        return app;
    }
}
