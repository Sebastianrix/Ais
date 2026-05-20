using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLayer.DTOs
{
    public class DataConsumerQueueDTO
    {
        public long Queue_Id { get; set; }
        public DateTime Source_Batch_Date { get; set; }
        public int Priority { get; set; }
        public string Requester { get; set; } = string.Empty;
        public string Status { get; set; } = "pending";
        public DateTime Created_At { get; set; }
    }
}
