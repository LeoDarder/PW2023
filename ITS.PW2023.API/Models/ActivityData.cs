namespace ITS.PW2023.Simulator.Models
{
    public class ActivityData
    {
        public Guid IdDevice { get; set; }
        public Activity Activity { get; set; }
        public int Heartbeat { get; set; }
        public Position Position { get; set; }
        public int Laps { get; set; }
        public ActivityData(Guid device, Activity activity, int heartbeat, Position position, int laps)
        {
            Device = device;
            Activity = activity;
            Heartbeat = heartbeat;
            Position = position;
            Laps = laps;
        }
    }
}
