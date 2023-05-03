namespace ITS.PW2023.API.Models
{
    public class ActivityData
    {
        public Guid IdDevice { get; set; }
        public Guid IdActivity { get; set; }
        public int Heartbeat { get; set; }
        public Position Position { get; set; }
        public int Laps { get; set; }
        public DateTime Time { get; set; }
        public ActivityData(Guid iddevice, Guid idactivity, int heartbeat, Position position, int laps, DateTime time)
        {
            IdDevice = iddevice;
            IdActivity = idactivity;
            Heartbeat = heartbeat;
            Position = position;
            Laps = laps;
            Time = time;
        }
    }
}
