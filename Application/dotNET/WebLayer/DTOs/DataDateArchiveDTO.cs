using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLayer.DTOs
{
    internal class DataDateArchiveDTO
    {
        public DateTime Source_Batch_Date { get; set; }
        public long Total_Rows { get; set; }
        public long Tanker_Rows { get; set; }
        public long Positions_Inserted { get; set; }
        public DateTime Archived_At { get; set; }
    }
}
