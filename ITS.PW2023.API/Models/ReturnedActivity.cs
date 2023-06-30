using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core.Flux.Domain;

namespace ITS.PW2023.API.Models
{
    public class ReturnedActivity
    {
        public Guid? IdDevice { get; set; }
        public DateTime Time { get; set; }
        public int Duration { get; set; }
        public int AvgHB { get; set; }
        public Position Position { get; set; }
        public int Laps { get; set; }

        public static List<ReturnedActivity> GetReturnedActivities(List<FluxTable> tables)
        {
            var groupedTables = tables.GroupBy(x => x.Records.First().GetValueByKey("Activity"));
            var ReturnedActivities = new List<ReturnedActivity>();


            foreach (var groupedtable in groupedTables)
            {
                var act = new ReturnedActivity();
                act.Position = new Position();
                DateTime minDate = DateTime.Now;
                DateTime maxDate = DateTime.MinValue;

                foreach(var table in groupedtable)
                {
                    var firstRecord = table.Records.First();

                    if (act.IdDevice is null)
                    {
                        act.IdDevice = new Guid(firstRecord.GetValueByKey("Activity").ToString());
                        act.Time = (DateTime)firstRecord.GetTimeInDateTime();
                        minDate = act.Time;
                        maxDate = act.Time;
                    }

                    DateTime minDateTimeRow = (DateTime)table.Records.Min(x => x.GetTimeInDateTime());
                    DateTime maxDateTimeRow = (DateTime)table.Records.Max(x => x.GetTimeInDateTime());
                    if (minDate > minDateTimeRow)
                    {
                        minDate = minDateTimeRow;
                    }
                    else if(maxDate < maxDateTimeRow)
                    {
                        maxDate = maxDateTimeRow;
                    }

                    if(String.Equals(firstRecord.GetField(), "HeartBeat"))
                    {
                        act.AvgHB = Convert.ToInt32(table.Records.Average(x =>(long) x.GetValueByKey("_value")));
                    }
                    else if (String.Equals(firstRecord.GetField(), "PositionX"))
                    {
                        act.Position.Longitude = (double) firstRecord.GetValueByKey("_value");
                    }
                    else if (String.Equals(firstRecord.GetField(), "PositionY"))
                    {
                        act.Position.Latitude = (double)firstRecord.GetValueByKey("_value");
                    }
                    else if (String.Equals(firstRecord.GetField(), "Laps"))
                    {
                        act.Laps = (int) table.Records.Max(x =>(long) x.GetValueByKey("_value"));
                    }
                }

                act.Duration = Convert.ToInt32((maxDate - minDate).TotalMinutes);

                ReturnedActivities.Add(act);
            }

            return ReturnedActivities;
        }
    }
}
