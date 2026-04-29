using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Voyage
    {
        public int? voyage_id { get; set; }
        public int? tanker_id { get; set; }
        public string? voyage_status { get; set; }
        public DateTime? start_time_utc { get; set; }
        public DateTime? end_time_utc { get; set; }
        public int? start_position_id { get; set; }
        public int? end_position_id { get; set; }
        public string? start_port_name { get; set; }
        public string? end_port_name { get; set; }
        public string? destination_final {  get; set; }
        public DateTime? eta_final { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
