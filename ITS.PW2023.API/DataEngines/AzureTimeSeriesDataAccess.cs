using Newtonsoft.Json;

namespace ITS.PW2023.API.DataEngines
{
    public class AzureTimeSeriesDataAccess
    {
        //https://learn.microsoft.com/en-us/rest/api/time-series-insights/dataaccessgen2/query/execute?tabs=HTTP#querygetseriespage1

        // Replace the following values with your own information
        private const string TenantId = "<your-tenant-id>";
        private const string EnvironmentFqdn = "<your-environment-fqdn>";
        private const string ClientId = "<your-client-id>";
        private const string ClientSecret = "<your-client-secret>";

        static async Task Main(string[] args)
        {
            // Authenticate and get access token
            var accessToken = await GetAccessToken();

            // Build the query
            var query = new
            {
                datasetId = "<your-dataset-id>",
                searchSpan = new
                {
                    from = "2022-01-01T00:00:00.000Z",
                    to = "2022-02-01T00:00:00.000Z"
                },
                filter = "DeviceId = 'Device001'",
                aggregate = new
                {
                    keys = new[] { "Temperature" },
                    apply = new[]
                    {
                        new { aggregate = "avg", input = "Temperature" },
                        new { aggregate = "sum", input = "Humidity" }
                    }
                }
            };

            // Send the query to Time Series Insights
            var result = await SendPostRequest(query, accessToken);

            // Parse the result
            var parsedResult = JsonConvert.DeserializeObject<dynamic>(result);

            // Display the result
            Console.WriteLine(parsedResult);
            Console.ReadLine();
        }

        private static async Task<string> GetAccessToken()
        {
            using (var httpClient = new HttpClient())
            {
                var url = $"https://login.microsoftonline.com/{TenantId}/oauth2/token";
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var content = new StringContent($"grant_type=client_credentials&client_id={ClientId}&client_secret={ClientSecret}&resource=https://{EnvironmentFqdn}", Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await httpClient.PostAsync(url, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                var token = JsonConvert.DeserializeObject<dynamic>(responseContent).access_token;
                return token;
            }
        }

        private static async Task<string> SendPostRequest(dynamic query, string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                var url = $"https://{EnvironmentFqdn}/timeseries/query?$orderBy=Timestamp%20asc";
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }
        }
    }
}
