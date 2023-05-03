namespace ITS.PW2023.Simulator.Models
{
    public class ActivityData
    {
        public Guid IdDevice { get; set; }
        public Guid IdActivity { get; set; }
        public int Heartbeat { get; set; }
        public Position Position { get; set; }
        public int Laps { get; set; }
        public DateTime Time { get; set; }
        public ActivityData(Guid device, Activity activity, int heartbeat, Position position)
        {
            IdDevice = device;
            IdActivity = activity.guid;
            Heartbeat = heartbeat;
            Position = position;
            Laps = activity.Laps;
            Time = DateTime.Now;
        }
    }
}
