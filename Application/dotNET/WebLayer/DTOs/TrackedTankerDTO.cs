using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLayer.DTOs
{
    public class TrackedTankerDTO
    {
        public int? Tracked_Id { get; set; }
        public string? Imo { get; set; }
        public string? source_trial { get; set; }
        public string? notes { get; set; }
        public bool? is_active { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
