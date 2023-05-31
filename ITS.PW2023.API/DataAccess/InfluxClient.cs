using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;
using InfluxDB.Client.Core.Flux.Domain;
using ITS.PW2023.API.Models;

namespace ITS.PW2023.API.DataAccess
{
    public class InfluxClient
    {
        private const string _queryAvgHB = "from(bucket: \"ActivitiesMonitor\")\r\n  |> range(start: 0)\r\n  |> filter(fn: (r) => r._measurement == \"ActivitiesMonitor\")\r\n  |> filter(fn: (r) => r._field == \"HeartBeat\")\r\n  |> filter(fn: (r) => r.Device == \"7266b21b-7f83-4204-86f7-d7c2d615edac\")";
        private const string _queryAvgLaps = "from(bucket: \"ActivitiesMonitor\")\r\n  |> range(start: 0)\r\n  |> filter(fn: (r) => r._measurement == \"ActivitiesMonitor\")\r\n  |> filter(fn: (r) => r._field == \"Laps\")\r\n  |> filter(fn: (r) => r.Device == \"7266b21b-7f83-4204-86f7-d7c2d615edac\")";
        private const string _queryActivities = "from(bucket: \"ActivitiesMonitor\")\r\n    |> range(start: 0)\r\n    |> filter(fn: (r) => r._measurement == \"ActivitiesMonitor\")\r\n    |> filter(fn: (r) => r.Device == \"%%DEVICEHERE%%\")";
        private const string _queryActivity = "from(bucket: \"ActivitiesMonitor\")\r\n    |> range(start: 0)\r\n    |> filter(fn: (r) => r._measurement == \"ActivitiesMonitor\")\r\n    |> filter(fn: (r) => r.Device == \"%%DEVICEHERE%%\")\r\n    |> filter(fn: (r) => r.Activity == \"%%ACTIVITYHERE%%\")";
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
                if (data.Heartbeat < 20 || data.Heartbeat > 200 || data.Position.Latitude < -90 || data.Position.Latitude > 90 || data.Position.Longitude < -180 || data.Position.Longitude > 180) return Results.Problem("Almeno un dato generato contiene un errore");
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

        public async Task<IResult> ReadActivities(string devGUID)
        {
            try
            {
                using var client = Client;

                string query = _queryActivities.Replace("%%DEVICEHERE%%", devGUID);

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
                using var client = Client;

                string query = _queryAvgHB.Replace("%%DEVICEHERE%%", devGUID);

                var readapi = client.GetQueryApi();
                List<FluxTable> tables = await readapi.QueryAsync(query, Org);

                int totalHB = 0;
                int instancesHB = 0;

                foreach(var table in tables)
                {
                    table.Records.ForEach(record =>
                    {
                        totalHB += Int32.Parse(record.GetValueByKey("_value").ToString());
                        instancesHB++;
                    });
                }

                return Results.Ok(Convert.ToInt32(totalHB / instancesHB));
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
                using var client = Client;

                string query = _queryAvgLaps.Replace("%%DEVICEHERE%%", devGUID);

                var readapi = client.GetQueryApi();
                List<FluxTable> tables = await readapi.QueryAsync(query, Org);

                int totallaps = 0;

                foreach (var table in tables)
                {
                    totallaps += Convert.ToInt32(table.Records.Max(record => record.GetValueByKey("_value")));
                }

                return Results.Ok((totallaps / tables.Count).ToString("0.0"));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
