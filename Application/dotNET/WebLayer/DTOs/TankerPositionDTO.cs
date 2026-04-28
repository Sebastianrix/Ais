using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace WebLayer.DTOs
{
    public class TankerPositionDTO
    {
        public int? Position_Id { get; set; }
        public int? Tanker_Id { get; set; }
        public int? Voyage_Id { get; set; }
        public int? Staging_Id { get; set; }
        public DateTime Timestamp { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string? Raw_Imo { get; set; }
        public string? Imo_Status { get; set; }
        public string? Raw_Mmsi { get; set; }
        public string? Mmsi_Status { get; set; }
        public Boolean Anomaly_Flag { get; set; } 
        public string? Navigational_Status { get; set; }
        public double? Rot {  get; set; }
        public double? Sog {  get; set; }
        public double? Cog { get; set; }
        public double? Heading { get; set; }
        public double? Draught { get; set; }
        public string? Destination { get; set; }
        public DateTime? Eta { get; set; } 
        public string? Position_Fixing_Device { get; set; }
        public string Data_Source_Type { get; set; }
        public DateTime Created_At { get; set; }       
    }
}
