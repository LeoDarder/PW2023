﻿using InfluxDB.Client.Core.Flux.Domain;

namespace ITS.PW2023.API.Models
{
    public class ReturnedRows
    {
        public DateTime Date { get; set; }
        public double Duration { get; set; }
        public Position Position { get; set; }
        public long Laps { get; set; }

        public List<HBInstance> HBInstances { get; set; }

        public static ReturnedRows GetRows(List<FluxTable> tables)
        {
            ReturnedRows rows = new ();
            rows.Position = new Position();

            foreach (var table in tables) 
            {
                var firstRecord = table.Records.First();

                if (rows.Date == DateTime.MinValue)
                {
                    rows.Date = (DateTime)firstRecord.GetTimeInDateTime();
                }

                if (String.Equals(firstRecord.GetField(), "PositionX"))
                {
                    rows.Position.Longitude = (double)firstRecord.GetValueByKey("_value");
                }
                else if (String.Equals(firstRecord.GetField(), "PositionY"))
                {
                    rows.Position.Latitude = (double)firstRecord.GetValueByKey("_value");
                }
                else if (String.Equals(firstRecord.GetField(), "Laps"))
                {
                    rows.Laps = table.Records.Max(x => (long)x.GetValueByKey("_value"));
                }
                else if (String.Equals(firstRecord.GetField(), "HeartBeat"))
                {
                    var heartBeats = new List<HBInstance>();

                    table.Records.ForEach(x => heartBeats.Add(new HBInstance((long)x.GetValueByKey("_value"), (DateTime)x.GetTimeInDateTime())));

                    rows.HBInstances = heartBeats;
                }
            }

            return rows;
        }
    }
}
