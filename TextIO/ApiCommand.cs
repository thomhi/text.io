using DataManager;
using DataManager.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TextIO.Events;

namespace TextIO;

public static class ApiCommand
{
    public static WebApplication AddCommandApi(this WebApplication app)
    {
        app.MapPost("/create-room/{name}", (string name, TextDbContext ctx, EventStore evtStore) =>
        {
            evtStore.StoreEvent(
                new Event<RoomCreated>()
                {
                    Type = nameof(RoomCreated),
                    Subject = "Host",
                    TimeStamp = DateTime.UtcNow,
                    Data = new RoomCreated() { RoomName = name }
                }
            );

            ctx.Rooms.Add(new Room { Name = name, CurrentWordToGuess = null });
            ctx.SaveChanges();
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


        app.MapPost("/guess-word/{roomName}/{user}/{guess}", Results<Ok<string>, NotFound<string>> (TextDbContext ctx, EventStore eventStore, string guess, string user, string roomName) =>
        {
            eventStore.StoreEvent(new Event<WordGuessed>()
            {
                Type = nameof(WordGuessed),
                Subject = "Player",
                TimeStamp = DateTime.UtcNow,
                Data = new WordGuessed() { Word = guess, RoomName = roomName, User = user }
            });
            if (ctx.Rooms.Any(r => r.Name == roomName && r.CurrentWordToGuess == guess))
            {
                return TypedResults.Ok("Correct");
            }
            if (ctx.Rooms.Any(r => r.Name == roomName))
            {
                return TypedResults.Ok("Incorrect");
            }
            return TypedResults.NotFound("Room does not exist");
        });


        //app.MapPost("/describe/{roomName}/{user}/{description}", (TextDbContext ctx, EventStore eventStore, string description, string user, string roomName) =>
        //{
        //    eventStore.StoreEvent(new Event<WordDescribed>()
        //    {
        //        Type = nameof(WordDescribed),
        //        Subject = "Player",
        //        TimeStamp = DateTime.UtcNow,
        //        Data = new WordDescribed() { Description = description, RoomName = roomName, User = user }
        //    });
        //    if (ctx.Rooms.Any(r => r.Name == roomName))
        //    {
        //        return TypedResults.Ok("Transmitted");
        //    }
        //    return TypedResults.Problem("Room does not exist");
        //});

        app.MapPost("/add-word-to-wordlist/{word}", (string word, TextDbContext ctx) =>
        {
            ctx.Words.Add(new Words { Word = word });
            ctx.SaveChanges();
        });

        app.MapPost("/clean-up", (TextDbContext ctx) =>
        {
            ctx.Database.ExecuteSqlRaw("DELETE FROM Words");
            ctx.Database.ExecuteSqlRaw("DELETE FROM Rooms");
        });
        return app;
    }
}
