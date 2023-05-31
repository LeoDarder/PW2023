using InfluxDB.Client.Core.Flux.Domain;

namespace ITS.PW2023.API.Models
{
    public class ReturnedActivity
    {
        public string Guid { get; set; }
        public DateTime Date { get; set; }
        public double Duration { get; set; }
        public int AvgHB { get; set; }
        public Position Position { get; set; }
        public long Laps { get; set; }

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

                    if (act.Guid is null)
                    {
                        act.Guid = firstRecord.GetValueByKey("Activity").ToString();
                        act.Date = (DateTime)firstRecord.GetTimeInDateTime();
                        minDate = act.Date;
                        maxDate = act.Date;
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
                        act.Laps = table.Records.Max(x =>(long) x.GetValueByKey("_value"));
                    }
                }

                ReturnedActivities.Add(act);
            }

            return ReturnedActivities;
        }
    }
}
