using InfluxDB.Client.Core.Flux.Domain;

namespace ITS.PW2023.API.Models
{
    public class ReturnedError
    {
        public string DevGUID { get; set; }
        public string ActGUID { get; set; }
        public string Field { get; set; }
        public string Data { get; set; }
        public DateTime Time { get; set; }

        public static List<ReturnedError> GetReturnedErrors(List<FluxTable> tables)
        {
            List<ReturnedError> returnedErrors = new();

            foreach (var table in tables)
            {
                foreach(var row in table.Records)
                {
                    returnedErrors.Add(new ReturnedError
                    {
                        DevGUID = row.GetValueByKey("Device").ToString(),
                        ActGUID = row.GetValueByKey("Activity").ToString(),
                        Field = row.GetField(),
                        Data = row.GetValue().ToString(),
                        Time = (DateTime)row.GetTimeInDateTime()
                    }) ;
                }
            }

            return returnedErrors;
        }
    }
}
