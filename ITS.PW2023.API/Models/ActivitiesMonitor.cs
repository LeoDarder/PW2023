using InfluxDB.Client.Core;

namespace ITS.PW2023.API.Models
{
    [Measurement("ActivitiesMonitor")]
    public class ActivitiesMonitor
    {
        [Column("Device", IsTag = true)] public string IdDevice { get; set; }
        [Column("Activity", IsTag = true)] public string IdActivity { get; set; }
        [Column("PositionX")] public double PositionX { get; set; }
        [Column("PositionY")] public double PositionY { get; set; }
        [Column("HeartBeat")] public int HeartBeat { get; set; }
        [Column("Laps")] public int Laps { get; set; }
        [Column(IsTimestamp = true)] public DateTime Time { get; set; }

        public ActivitiesMonitor(ActivityData data)
        {
            IdDevice = data.IdDevice.ToString();
            IdActivity = data.IdActivity.ToString();
            PositionX = data.Position.Longitude;
            PositionY = data.Position.Latitude;
            HeartBeat = data.Heartbeat;
            Laps = data.Laps;
            Time = data.Time;
        }
    }
}
