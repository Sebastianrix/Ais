using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLayer.DTOs
{
    public class VesselMapPositionDTO
    {
        public long Tanker_Id { get; set; }
        public string? Mmsi { get; set; }
        public string? Vessel_Name { get; set; }
        public string? Ship_Type { get; set; }
        public string? Flag { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp_Utc { get; set; }
        public double? Sog { get; set; }
        public double? Cog { get; set; }
        public double? Heading { get; set; }
        public string? Navigational_Status { get; set; }
        public bool Is_Anomalous { get; set; }   // flag=UN, basiclly Mmsi spoofed non-exsisting country. 
    }
}