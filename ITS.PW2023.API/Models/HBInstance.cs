namespace ITS.PW2023.API.Models
{
    public class HBInstance
    {
        public long HeartBeat { get; set; }
        public DateTime Time { get; set; }

        public HBInstance(long heartBeat, DateTime time)
        {
            HeartBeat = heartBeat;
            Time = time;
        }

        public HBInstance() { }
    }
}
