using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLayer.DTOs
{
    public class StatsDTO
    {
        public int TankerCount { get; set; }
        public int PositionCount { get; set; }
        public int TrackedTankerCount { get; set; }
        public int StagingCount { get; set; }
    }
}
