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

app.MapPost("/writeData", (ActivityData data, InfluxClient influx) =>
{
    return influx.WriteData(data);
});

app.MapGet("/getActivities", (string devGUID, InfluxClient influx) =>
{
    return influx.ReadActivities(devGUID);
});

app.MapGet("/getRows", (string devGUID, string actGUID, InfluxClient influx) =>
{
    return influx.ReadRows(devGUID, actGUID);
});
app.MapGet("/getAvgHB", (string devGUID, InfluxClient influx) =>
{
    return influx.GetAvgHB(devGUID);
});

app.MapGet("/getAvgLaps", (string devGUID, InfluxClient influx) =>
{
    return influx.GetAvgLaps(devGUID);
});

app.MapGet("/getErrors", (InfluxClient influx) =>
{
    return influx.GetErrors();
});

app.MapGet("/getUser", (string username, string password, UserServices userServices) =>
{
    var urs = userServices.GetUserData(username, password);

    if (urs.Any())
    {
        return Results.Ok(urs);
    }
    else
    {
        return Results.BadRequest("Credentials not valid");
    }
});

app.MapPost("/changeDevName", (string devName, string devGUID, UserServices userServices) =>
{
        userServices.PostDeviceName(devGUID, devName);
        return Results.Ok();
});

app.Run();
