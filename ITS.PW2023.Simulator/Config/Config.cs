using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS.PW2023.Simulator.Models
{
    public class Config
    {
        public const string ConfigPosition = "Config";
        public Heartbeat Heartbeat { get; set; }
        public Position Position { get; set; }
        public Laptime LapTime { get; set; }
        public int[] PoolLenghts { get; set; }
    }

    public class Heartbeat
    {
        public int Max { get; set; }
        public int Min { get; set; }
        public int HighLimit { get; set; }
        public int LowLimit { get; set; }
        public int ErrorRate { get; set; }
    }

    public class Position
    {
        public int ErrorRate { get; set; }
    }

    public class Laptime
    {
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
