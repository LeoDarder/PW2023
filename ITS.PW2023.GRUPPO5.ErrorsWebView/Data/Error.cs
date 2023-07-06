using Npgsql;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ITS.PW2023.GRUPPO5.ErrorsWebView.Data
{
    public class Error
    {
        public string devGUID { get; set; }
        public string actGUID { get; set; }
        public string field { get; set; }
        public string data { get; set; }

        public int? batch { get; set; } 

        private const string _endpoint = "https://cper-pw2023-gruppo5-api.azure-api.net/getErrors";
        private const string _connString = "Server=192.168.103.41;Port=5432;Database=earth4sport;User Id=gruppo5;Password=gruppo5;";
        private static string query = "SELECT batch FROM batchdevices WHERE device = '%%DEVICEHERE%%'";
        public static async Task<List<Error>> GetErrorsAsync()
        {
            List<Error> errors = new(); 
            using (HttpClient client = new())
            {
                //insert azure subkey line
                var json = await client.GetStringAsync(_endpoint);
                errors = JsonSerializer.Deserialize<List<Error>>(json);
            };

            List<string> ids = errors.DistinctBy(x => x.devGUID).Select(x => x.devGUID).ToList();
            Dictionary<string, int> batchdevices = new Dictionary<string, int>();

            using NpgsqlConnection conn = new(_connString);
            conn.Open();
            foreach (var id in ids)
            {
                NpgsqlCommand cmd = new(query.Replace("%%DEVICEHERE%%", id.ToUpper()), conn);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    batchdevices.Add(id, reader.GetInt32(0));
                }
                reader.Close();
                cmd.Dispose();
            }
            
            foreach (var error in errors)
            {
                if (batchdevices.TryGetValue(error.devGUID, out int value))
                {
                    error.batch = value;
                }
            }

            return errors;
        }
    }
}
