using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Stats
    {
        public int TankerCount { get; set; }
        public int PositionCount { get; set; }
        public int TrackedTankerCount { get; set; }
        public int StagingCount { get; set; }
    }
}
