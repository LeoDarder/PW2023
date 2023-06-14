using System.Net.Http.Headers;

namespace ITS.PW2023.GRUPPO5.ErrorsWebView.Data
{
    public class Error
    {
        public string DevGUID { get; set; }
        public string ActGUID { get; set; }
        public string Field { get; set; }
        public string Data { get; set; }

        public static List<Error> Errors()
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

        }
    }
}
