using System.Reflection.Metadata;
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
        private readonly int[] PoolLenght = { 25, 50 };
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
