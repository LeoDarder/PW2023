using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS.PW2023.Simulator.Models
{
    public class Activity
    {
        public Guid guid = Guid.Empty;
        public int Laps = 0;
        public Activity(Guid activityGuid)
        {
            guid = activityGuid;
        }
    }
}
