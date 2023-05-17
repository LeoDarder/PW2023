﻿using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;
using InfluxDB.Client.Core.Flux.Domain;
using ITS.PW2023.API.Models;

namespace ITS.PW2023.API.DataAccess
{
    public class InfluxClient
    {
        private InfluxDBClient Client { get; set; }
        private string Bucket { get; set; }
        private string Org { get; set; }
        public InfluxClient(IConfiguration configuration)
        {
            var token = configuration.GetConnectionString("Token");
            Bucket = "ActivitiesMonitor";
            Org = "Gruppo 5 Project Work 2023";

            Client = new InfluxDBClient("https://westeurope-1.azure.cloud2.influxdata.com", token);
        }

        public IResult WriteData(ActivityData data)
        {
            try
            {
                using var client = Client;

                var activityData = new ActivitiesMonitor(data);

                using var writeApi = client.GetWriteApi();
                writeApi.WriteMeasurement(activityData, WritePrecision.Ns, Bucket, Org);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [Measurement("ActivitiesMonitor")]
        private class ActivitiesMonitor
        {
            [Column("Device", IsTag = true)] public string IdDevice { get; set; }
            [Column("Activity", IsTag = true)] public string IdActivity { get; set; }
            [Column("PositionX")] public double PositionX { get; set; }
            [Column("PositionY")] public double PositionY { get; set; }
            [Column("HeartBeat")] public int HeartBeat { get; set; }
            [Column("Laps")] public int Laps { get; set; }
            [Column(IsTimestamp = true)] public DateTime Time { get; set; }

            public ActivitiesMonitor(ActivityData data)
            {
                IdDevice = data.IdDevice.ToString();
                IdActivity = data.IdActivity.ToString();
                PositionX = data.Position.Longitude;
                PositionY = data.Position.Latitude;
                HeartBeat = data.Heartbeat;
                Laps = data.Laps;
                Time = data.Time;
            }
        }

        public async Task<IResult> ReadActivities(string devGUID)
        {
            try
            {
                using var client = Client;

                string query = System.IO.File.ReadAllText("./Queries/Activities.txt").Replace("%%DEVICEHERE%%", devGUID);

                var readapi = client.GetQueryApi();
                List<FluxTable> table = await readapi.QueryAsync(query, Org);
                List<ReturnedActivity> activities = ReturnedActivity.GetReturnedActivities(table);

                return Results.Ok(activities);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public async Task<IResult> ReadRows(string devGUID, string actGUID)
        {
            try
            {
                using var client = Client;

                string query = System.IO.File.ReadAllText("./Queries/Activity.txt").Replace("%%DEVICEHERE%%", devGUID).Replace("%%ACTIVITYHERE%%", actGUID);

                var readapi = client.GetQueryApi();
                List<FluxTable> table = await readapi.QueryAsync(query, Org);
                ReturnedRows rows = ReturnedRows.GetRows(table);

                return Results.Ok(rows);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
