using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class TrackedTanker
    {
        public int? Tracked_Id { get; set; }
        public string? Imo { get; set; }
        public string? Source_Trial { get; set; }
        public string? Notes { get; set; }
        public bool? Is_Active { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }
}
