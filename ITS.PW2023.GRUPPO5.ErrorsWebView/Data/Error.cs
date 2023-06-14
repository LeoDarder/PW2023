using System.Net.Http.Headers;

namespace ITS.PW2023.GRUPPO5.ErrorsWebView.Data
{
    public class Error
    {
        public string DevGUID { get; set; }
        public string ActGUID { get; set; }
        public string Field { get; set; }
        public string Data { get; set; }

        //public static async List<Error> GetErrors()
        //{
        //    using HttpClient client = new();
        //    var json = await client.GetStringAsync("localhost:7030/GetErrors");
            
        //}
    }
}
