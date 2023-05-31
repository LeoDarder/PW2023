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
        public HeartbeatConfig Heartbeat { get; set; }
        public PositionConfig Position { get; set; }
        public LapConfig Lap { get; set; }
        public int[] PoolLenghts { get; set; }
    }

    public class HeartbeatConfig
    {
        public int Max { get; set; }
        public int Min { get; set; }
        public int HighLimit { get; set; }
        public int LowLimit { get; set; }
        public int ErrorRate { get; set; }
    }

    public class PositionConfig
    {
        public int ErrorRate { get; set; }
    }

    public class LapConfig
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int LapsCount { get; set; }
    }
}
