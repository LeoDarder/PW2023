using ITS.PW2023.Simulator.Models;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace ITS.PW2023.Simulator.Models
{
    public class Device
    {
        public Guid guid { get; set; }

        private readonly int MaxHeartbeat;
        private int MinHeartbeat;                 //valore non usato
        private readonly int LowHeartbeatLimit;
        private readonly int HighHeartbeatLimit;
        private readonly int HeartbeatErrorLimit;
        public Activity? CurrentActivity { get; set; }
        public Device(Guid deviceGuid, Config config)
        {
            guid = deviceGuid;
            MaxHeartbeat = config.Heartbeat.Max;
            MinHeartbeat = config.Heartbeat.Min;
            LowHeartbeatLimit = config.Heartbeat.LowLimit;
            HighHeartbeatLimit = config.Heartbeat.HighLimit;
            HeartbeatErrorLimit = config.Heartbeat.ErrorRate;
            CurrentActivity = null;
        }
        public Guid StartNewActivity()
        {
            Guid guid = Guid.NewGuid();
            Random rand = new Random();
            CurrentActivity = new Activity(guid, PoolLenght[rand.Next(PoolLenght.Length)]);
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
            //genera tutto a caso, senza criterio
            if (Random.Shared.Next(0, 100) <= HeartbeatErrorLimit)
            {
                generatedHeartbeat = Random.Shared.Next(0, MaxHeartbeat - normalHeartbeatInterval);
                if (generatedHeartbeat > LowHeartbeatLimit) generatedHeartbeat += normalHeartbeatInterval;
            }
            else generatedHeartbeat = Random.Shared.Next(LowHeartbeatLimit, HighHeartbeatLimit);

            return generatedHeartbeat;
        }
        private Position GeneratePosition()              
        {
            //TODO: genera anche valori non validi

            ////20% di chance di generare delle coordinate anomale (20% per testare)
            //if (Random.Shared.Next(0, 100) <= 20)
            //{

            //}
            //else
            //{
            //    Random rand = new Random();
            //    // Genero una distanza casuale
            //    double distance = rand.NextDouble();

            //    // Calcolo delle coordinate randomiche
            //    double latitude = (1 - distance) * CurrentActivity.PoolStart.Latitude + distance * CurrentActivity.PoolEnd.Latitude;
            //    double longitude = (1 - distance) * CurrentActivity.PoolStart.Longitude + distance * CurrentActivity.PoolEnd.Longitude;
            //}

            Random rand = new Random();
            // Genero una distanza casuale
            double distance = rand.NextDouble();

            // Calcolo delle coordinate randomiche
            double latitude = (1 - distance) * CurrentActivity.PoolStart.Latitude + distance * CurrentActivity.PoolEnd.Latitude;
            double longitude = (1 - distance) * CurrentActivity.PoolStart.Longitude + distance * CurrentActivity.PoolEnd.Longitude;

            return new Position(latitude, longitude);
        }
    }
}
