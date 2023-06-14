using System.Net.Http.Headers;
using System.Text.Json;

namespace ITS.PW2023.GRUPPO5.ErrorsWebView.Data
{
    public class Error
    {
        public string devGUID { get; set; }
        public string actGUID { get; set; }
        public string field { get; set; }
        public string data { get; set; }

        public static async Task<List<Error>> GetErrorsAsync()
        {
            try
            {
                using HttpClient client = new();
                //client.DefaultRequestHeaders.Accept.Clear();

                var json = await client.GetStringAsync("https://localhost:7030/getErrors");
                return JsonSerializer.Deserialize<List<Error>>(json);
            }
            catch
            {
                return new List<Error>();
            }
        }
    }
}
