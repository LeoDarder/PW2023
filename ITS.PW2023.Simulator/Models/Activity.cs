using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ITS.PW2023.Simulator.Models
{
    public class Activity
    {
        public Guid guid = Guid.Empty;
        public int PreviousHeartbeat = -1;
        public int LapsGenCount = 0;
        public int Laps = 0;
        public Position PoolStart { get; set; }
        public Position PoolEnd { get; set; }
        public Activity(Guid activityGuid, int poolLenght)
        {
            guid = activityGuid;

            // Coordinate di partenza
            PoolStart = new Position(Math.Round((Random.Shared.NextDouble() - 0.5) * 180, 6), Math.Round((Random.Shared.NextDouble() - 0.5) * 360, 6));

            // Generazione casuale di una direzione in radianti
            Random rand = new Random();
            double direction = rand.NextDouble() * 2 * Math.PI;

            // Conversione della distanza in gradi di latitudine/longitudine
            double radius = 6371000; // Raggio medio della Terra in metri
            double latDiff = (poolLenght / radius) * (180 / Math.PI);
            double lonDiff = latDiff / Math.Cos(Math.PI * PoolStart.Latitude / 180);

            // Calcolo delle coordinate finali
            PoolEnd = new Position(PoolStart.Latitude + latDiff * Math.Cos(direction), PoolStart.Longitude + lonDiff * Math.Sin(direction));

        }
    }
}
