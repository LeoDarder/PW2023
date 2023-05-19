using System.Text.Json;

namespace ITS.PW2023.Simulator.Models
{
    public class Device
    {
        public Guid guid { get; set; }

        private readonly int MaxHeartbeat;
        private int MinHeartbeat;                 //valore non usato
        private readonly int LowHeartbeatLimit = 30;
        private readonly int HighHeartbeatLimit = 200;
        public Activity? CurrentActivity { get; set; }
        public Device(Guid deviceGuid, int maxheartbeat = 300, int minheartbeat = 0)
        {
            guid = deviceGuid;
            MaxHeartbeat = maxheartbeat;
            MinHeartbeat = minheartbeat;
            CurrentActivity = null;
        }
        public Guid StartNewActivity()
        {
            Guid guid = Guid.NewGuid();
            CurrentActivity = new Activity(guid);
            return guid;
        }
        public Activity? GetCurrentActivity() { return CurrentActivity; }
        public void EndNewActivity() { CurrentActivity = null; }
        public ActivityData? GenerateActivityData()
        {
            if (CurrentActivity == null)
            {
                Console.WriteLine("No activity was started. Starting new activity...\n");
                Console.WriteLine($"Device '{guid}' created a new activity: '{StartNewActivity()}'");
            }
            return new ActivityData(guid, CurrentActivity, GenerateHeartBeat(), GeneratePosition());
        }
        private int GenerateHeartBeat()
        {
            int normalHeartbeatInterval = HighHeartbeatLimit - LowHeartbeatLimit;
            int generatedHeartbeat;
            //20% di chance di generare un battito anomalo (20% per testare)
            //genera tutto a caso, senza criterio
            if (Random.Shared.Next(0, 100) <= 20)
            {
                generatedHeartbeat = Random.Shared.Next(0, MaxHeartbeat - normalHeartbeatInterval);
                if (generatedHeartbeat > LowHeartbeatLimit) generatedHeartbeat += normalHeartbeatInterval;
            }
            else generatedHeartbeat = Random.Shared.Next(LowHeartbeatLimit, HighHeartbeatLimit);

            return generatedHeartbeat;
        }
        private Position GeneratePosition()              //TODO: genera anche valori non validi
        {
            double latitude = (Random.Shared.NextDouble() - 0.5) * 180;
            double longitude = (Random.Shared.NextDouble() - 0.5) * 360;
            return new Position(latitude, longitude);
        }

    }
}
