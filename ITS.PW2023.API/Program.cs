using ITS.PW2023.API.DataAccess;
using ITS.PW2023.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<InfluxClient>();

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

app.MapPost("/writeData", (ActivityData data) =>
{
    InfluxClient influx = new(builder.Configuration);
    return influx.WriteData(data);
});

app.MapGet("/getActivities", (string devGUID) =>
{
    InfluxClient influx = new(builder.Configuration);
    return influx.ReadActivities(devGUID);
});

app.MapGet("/getRows", (string devGUID, string actGUID) =>
{
    InfluxClient influx = new(builder.Configuration);
    return influx.ReadRows(devGUID, actGUID);
});

app.MapGet("/getUserData", (string username, string password) =>
{
    UserServices userServices = new(builder.Configuration);
    return userServices.GetUserData(username, password);
});

app.MapGet("/getAvgHB", (string devGUID) =>
{
    InfluxClient influx = new(builder.Configuration);
    return influx.GetAvgHB(devGUID);
});

app.MapGet("/getAvgLaps", (string devGUID) =>
{
    InfluxClient influx = new(builder.Configuration);
    return influx.GetAvgLaps(devGUID);
});

app.Run();
