using ITS.PW2023.Simulator.Models;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Transactions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ITS.PW2023.Simulator.Models
{
    public class Device
    {
        public Guid guid { get; set; }

        private readonly int MaxHeartbeat;
        private int MinHeartbeat;                 //valore non usato
        private readonly int LowHeartbeatLimit;
        private readonly int HighHeartbeatLimit;
        private readonly int HeartbeatErrorRate;
        private readonly int PositionErrorRate;
        private readonly int[] PoolLenghts;
        public Activity? CurrentActivity { get; set; }
        public Device(Guid deviceGuid, Config config)
        {
            guid = deviceGuid;

            MaxHeartbeat = config.Heartbeat.Max;
            MinHeartbeat = config.Heartbeat.Min;
            LowHeartbeatLimit = config.Heartbeat.LowLimit;
            HighHeartbeatLimit = config.Heartbeat.HighLimit;
            HeartbeatErrorRate = config.Heartbeat.ErrorRate;

            PoolLenghts = config.PoolLenghts.ToArray();

            PositionErrorRate = config.Position.ErrorRate;

            CurrentActivity = null;
        }
        public Guid StartNewActivity()
        {
            Guid guid = Guid.NewGuid();
            Random rand = new Random();
            CurrentActivity = new Activity(guid, PoolLenghts[rand.Next(PoolLenghts.Length)]);
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
            GenerateLaps();
            return new ActivityData(guid, CurrentActivity, GenerateHeartBeat(), GeneratePosition());
        }
        private int GenerateHeartBeat()
        {
            Random rand = new Random();
            int normalHeartbeatInterval = HighHeartbeatLimit - LowHeartbeatLimit;
            int generatedHeartbeat;

            if (rand.Next(0, 100) <= HeartbeatErrorRate)
            {
                generatedHeartbeat = GenerateError<int>(MinHeartbeat, MaxHeartbeat, LowHeartbeatLimit, HighHeartbeatLimit);
            }
            else
                generatedHeartbeat = CurrentActivity.PreviousHeartbeat == -1 ? rand.Next(LowHeartbeatLimit, HighHeartbeatLimit) : CurrentActivity.PreviousHeartbeat + rand.Next(-5, 6);

            CurrentActivity.PreviousHeartbeat = generatedHeartbeat;
            return generatedHeartbeat;
        }
        private Position GeneratePosition()              
        {
            double latitude = 0, longitude = 0;
            Random rand = new Random();

            if (Random.Shared.Next(0, 100) <= PositionErrorRate)
            {
                // Generazione valori non validi

                latitude = Math.Round(GenerateError<double>(-500, 500, -90, 90), 6);

                longitude = Math.Round(GenerateError<double>(-500, 500, -180, 180), 6);
            }
            else
            {
                //Generazione valori validi

                // Genero una distanza casuale
                double distance = rand.NextDouble();

                // Calcolo delle coordinate randomiche
                latitude = Math.Round((1 - distance) * CurrentActivity.PoolStart.Latitude + distance * CurrentActivity.PoolEnd.Latitude, 6); ;
                longitude = Math.Round((1 - distance) * CurrentActivity.PoolStart.Longitude + distance * CurrentActivity.PoolEnd.Longitude, 6);
            }

            return new Position(latitude, longitude);
        }

        private void GenerateLaps()
        {
            if (CurrentActivity.LapsCount == 3)
            {
                CurrentActivity.Laps++;
                CurrentActivity.LapsCount = 0;
            }else
                CurrentActivity.LapsCount++;
        }

        // <summary>
        /// Funzione che genera degli errori secondo i parametri forniti
        /// </summary>
        /// <param name="Min">Minimo valore di errore da generare</param>
        /// <param name="Max">Massimo valore di errore da generare</param>
        /// <param name="MinNormal">Minimo valore nel range dei valori corretti (non generati)</param>
        /// <param name="MaxNormal">Massimo valore nel range dei valori corretti (non generati)</param>
        /// <returns>Ritorna il valore dell'errore generato nel tipo dichiarato (T)</returns>
        private T GenerateError<T>(double Min, double Max, double MinNormal, double MaxNormal)
        {
            Random rand = new Random();

            if (typeof(T) == typeof(int)) 
            {
                // Genero per tipi intero
                int value = rand.Next(0, (int)((Max - Min) - (MaxNormal - MinNormal)));
                if (value > MinNormal)
                    value += (int)(Max - Min);

                return (T)Convert.ChangeType(value, typeof(int));
            }
            else
            {
                double abs = Math.Abs(Min);
                // Genero per tipi double, spostando tutti i valori in modo che siano positivi
                Min += abs;
                Max += abs;
                MinNormal += abs;
                MaxNormal += abs;
                double value = (rand.NextDouble() * ((Max - Min) - (MaxNormal - MinNormal)));
                if (value > MinNormal)
                    value += (int)(Max - Min);
                // alla fine aggiungo il valore tolto prima
                value -= abs;
                return (T)Convert.ChangeType(value, typeof(double));
            } 
        }
    }
}
