using ITS.PW2023.API.DataAccess;
using ITS.PW2023.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<InfluxClient>();
builder.Services.AddSingleton<UserServices>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("cors-all",
                        policy =>
                        {
                            policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("cors-all");

app.MapPost("/writeData", async (ActivityData data, InfluxClient influx) =>
{
    return influx.WriteData(data);
});

app.MapGet("/getActivities", async (string devGUID, InfluxClient influx) =>
{
    return influx.ReadActivities(devGUID);
});

app.MapGet("/getRows", async (string devGUID, string actGUID, InfluxClient influx) =>
{
    return influx.ReadRows(devGUID, actGUID);
});

app.MapGet("/getUserData", (string username, string password, UserServices userServices) =>
{
    return userServices.GetUserData(username, password);
});

app.MapGet("/getAvgHB", (string devGUID, InfluxClient influx) =>
{
    return influx.GetAvgHB(devGUID);
});

app.MapGet("/getAvgLaps", (string devGUID, InfluxClient influx) =>
{
    return influx.GetAvgLaps(devGUID);
});

app.Run();
