using aspnetcore_rest_api;
using Microsoft.OpenApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("FitnessTrackerDb"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/workouts", async (AppDbContext db) =>
    await db.Workouts.ToListAsync());

app.MapGet("/workouts/{id}", async (int id, AppDbContext db) =>
    await db.Workouts.FindAsync(id)
        is Workout workout
            ? Results.Ok(workout)
            : Results.NotFound());

app.MapPost("/workouts", async (Workout workout, AppDbContext db) =>
{
    db.Workouts.Add(workout);
    await db.SaveChangesAsync();

    return Results.Created($"/workouts/{workout.WorkoutId}", workout);
});

app.MapPut("/workouts/{id}", async (int id, Workout inputWorkout, AppDbContext db) =>
{
    var workout = await db.Workouts.FindAsync(id);

    if (workout is null) return Results.NotFound();

    workout.ActivityType = inputWorkout.ActivityType;
    workout.DurationMinutes = inputWorkout.DurationMinutes;
    workout.DistanceKm = inputWorkout.DistanceKm;
    workout.CaloriesBurned = inputWorkout.CaloriesBurned;
    workout.WorkoutDate = inputWorkout.WorkoutDate;
    workout.Notes = inputWorkout.Notes;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/workouts/{id}", async (int id, AppDbContext db) =>
{
    if (await db.Workouts.FindAsync(id) is Workout workout)
    {
        db.Workouts.Remove(workout);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();