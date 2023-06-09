﻿using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core.Flux.Domain;
using ITS.PW2023.API.Models;
using Newtonsoft.Json.Linq;

namespace ITS.PW2023.API.DataAccess
{
    public class InfluxClient
    {
        private const string _queryAvgHB = "from(bucket: \"ActivitiesMonitor\")\r\n  |> range(start: 0)\r\n  |> filter(fn: (r) => r._measurement == \"ActivitiesMonitor\")\r\n  |> filter(fn: (r) => r._field == \"HeartBeat\")\r\n  |> filter(fn: (r) => r.Device == \"%%DEVICEHERE%%\")";
        private const string _queryAvgLaps = "from(bucket: \"ActivitiesMonitor\")\r\n  |> range(start: 0)\r\n  |> filter(fn: (r) => r._measurement == \"ActivitiesMonitor\")\r\n  |> filter(fn: (r) => r._field == \"Laps\")\r\n  |> filter(fn: (r) => r.Device == \"%%DEVICEHERE%%\")";
        private const string _queryActivities = "from(bucket: \"ActivitiesMonitor\")\r\n    |> range(start: 0)\r\n    |> filter(fn: (r) => r._measurement == \"ActivitiesMonitor\")\r\n    |> filter(fn: (r) => r.Device == \"%%DEVICEHERE%%\")";
        private const string _queryActivity = "from(bucket: \"ActivitiesMonitor\")\r\n    |> range(start: 0)\r\n    |> filter(fn: (r) => r._measurement == \"ActivitiesMonitor\")\r\n    |> filter(fn: (r) => r.Device == \"%%DEVICEHERE%%\")\r\n    |> filter(fn: (r) => r.Activity == \"%%ACTIVITYHERE%%\")";
        private const string _queryErrors = "from(bucket: \"SmartwatchErrors\")\r\n    |> range(start: 0)\r\n    |> filter(fn: (r) => r._measurement == \"ActivitiesMonitor\")\r\n";

        private readonly string Token;
        private const string Bucket = "ActivitiesMonitor";
        private const string ErrorsBucket = "SmartwatchErrors";
        private const string Org = "Gruppo 5 Project Work 2023";
        public InfluxClient(IConfiguration configuration)
        {
            Token = configuration.GetConnectionString("Token");
        }

        public async Task<IResult> WriteData(ActivityData data)
        {
            try
            {
                using var client = new InfluxDBClient("https://westeurope-1.azure.cloud2.influxdata.com", Token);
                var activityData = new ActivitiesMonitor(data);
                using var writeApi = client.GetWriteApi();

                if (data.Heartbeat < 30 || data.Heartbeat > 200 || data.Position.Latitude < -90 || data.Position.Latitude > 90 || data.Position.Longitude < -180 || data.Position.Longitude > 180)
                {
                    writeApi.WriteMeasurement(activityData, WritePrecision.Ns, ErrorsBucket, Org);
                    return Results.Ok();
                }

                writeApi.WriteMeasurement(activityData, WritePrecision.Ns, Bucket, Org);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public async Task<IResult> ReadActivities(string devGUID)
        {
            try
            {
                using var client = new InfluxDBClient("https://westeurope-1.azure.cloud2.influxdata.com", Token);

                string query = _queryActivities.Replace("%%DEVICEHERE%%", devGUID.ToLower());

                var readapi = client.GetQueryApi();
                List<FluxTable> table = await readapi.QueryAsync(query, Org);
                List<ReturnedActivity> activities = ReturnedActivity.GetReturnedActivities(table);

                return Results.Ok(activities.OrderByDescending(x => x.Time));
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
                using var client = new InfluxDBClient("https://westeurope-1.azure.cloud2.influxdata.com", Token);

                string query = _queryActivity.Replace("%%ACTIVITYHERE%%", actGUID);
                query = query.Replace("%%DEVICEHERE%%", devGUID);

                var readapi = client.GetQueryApi();
                List<FluxTable> tables = await readapi.QueryAsync(query, Org);
                ReturnedRows rows = ReturnedRows.GetRows(tables);

                return Results.Ok(rows);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public async Task<IResult> GetAvgHB(string devGUID)
        {
            try
            {
                using var client = new InfluxDBClient("https://westeurope-1.azure.cloud2.influxdata.com", Token);

                string query = _queryAvgHB.Replace("%%DEVICEHERE%%", devGUID);

                var readapi = client.GetQueryApi();
                List<FluxTable> tables = await readapi.QueryAsync(query, Org);

                int totalHB = 0;
                int instancesHB = 0;

                foreach(var table in tables)
                {
                    table.Records.ForEach(record =>
                    {
                        totalHB += int.Parse(record.GetValueByKey("_value").ToString());
                        instancesHB++;
                    });
                }

                if(instancesHB > 0)
                {
                    return Results.Ok(Convert.ToInt32(totalHB / instancesHB));
                }
                else
                {
                    return Results.Ok(0);
                }
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public async Task<IResult> GetAvgLaps(string devGUID)
        {
            try
            {
                using var client = new InfluxDBClient("https://westeurope-1.azure.cloud2.influxdata.com", Token);

                string query = _queryAvgLaps.Replace("%%DEVICEHERE%%", devGUID);

                var readapi = client.GetQueryApi();
                List<FluxTable> tables = await readapi.QueryAsync(query, Org);

                int totallaps = 0;

                foreach (var table in tables)
                {
                    totallaps += Convert.ToInt32(table.Records.Max(record => record.GetValueByKey("_value")));
                }

                if(tables.Count > 0)
                {
                    return Results.Ok(Math.Round((decimal)totallaps / tables.Count, 1));
                }
                else
                {
                    return Results.Ok(0);
                }
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public async Task<IResult> GetErrors()
        {
            try
            {
                using var client = new InfluxDBClient("https://westeurope-1.azure.cloud2.influxdata.com", Token);

                var readapi = client.GetQueryApi();
                List<FluxTable> tables = await readapi.QueryAsync(_queryErrors, Org);
                List<ReturnedError> errors = ReturnedError.GetReturnedErrors(tables);

                return Results.Ok(errors);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
