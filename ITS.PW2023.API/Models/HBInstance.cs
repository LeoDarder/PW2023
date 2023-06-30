namespace ITS.PW2023.API.Models
{
    public class HBInstance
    {
        public int HeartBeat { get; set; }
        public DateTime Time { get; set; }

        public HBInstance(long heartBeat, DateTime time)
        {
            HeartBeat = (int) heartBeat;
            Time = time;
        }

        public HBInstance() { }
    }
}
