using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class DataConsumerQueue
    {
        public long Queue_Id { get; set; }
        public DateTime Source_Batch_Date { get; set; }
        public int Priority { get; set; }
        public string Requester { get; set; }
        public string Status { get; set; }
        public DateTime Created_At { get; set; }
    }
}
