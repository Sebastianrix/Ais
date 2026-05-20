using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLayer.DTOs
{
    public class StatsDTO
    {
        // Redesigning, counting the raw data was out of hand (800 milion rows + )
        // From Tankers table. These are cheaper to count than staging
        public int TankerCount { get; set; }
        public int TrackedTankerCount { get; set; }

        // From DataDateArchive
        public long TotalPositionsProcessed { get; set; }
        public long TotalStagingRowsProcessed { get; set; }
        public int DatesProcessed { get; set; }
        public DateTime? LatestBatchDate { get; set; }
        public DateTime? OldestBatchDate { get; set; }

        // Queue status
        public int PendingBatches { get; set; }
        public int InProgressBatches { get; set; }
    }
}
