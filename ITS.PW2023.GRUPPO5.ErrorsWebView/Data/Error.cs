using System.Net.Http.Headers;
using System.Text.Json;

namespace ITS.PW2023.GRUPPO5.ErrorsWebView.Data
{
    public class Error
    {
        public string DevGUID { get; set; }
        public string ActGUID { get; set; }
        public string Field { get; set; }
        public string Data { get; set; }

        public static async Task<List<Error>> ErrorsAsync()
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("localhost:7030/GetErrors"));

            var json = await client.GetStringAsync(
                    "localhost:7030/GetErrors");
            var error = JsonSerializer.Deserialize<List<Error>>(json);

            return error;
        }
    }
}
