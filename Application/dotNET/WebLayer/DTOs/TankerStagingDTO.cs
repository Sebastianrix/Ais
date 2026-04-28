using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLayer.DTOs
{
    public class TankerStagingDTO
    {
        public int? Staging_Id { get; set; }
        public DateTime? Timestamp_Raw { get; set; }
        public string? Type_Of_Mobile { get; set; }
        public string? Mmsi { get; set; }
        public double Latitude_Raw { get; set; }
        public double Longitude_Raw { get; set; }
        public string? Navigational_Status { get; set; }
        public string? Rot_Raw { get; set; }
        public string? Sog_Raw { get; set; }
        public string? Cog_Raw { get; set; }
        public string? Heading_Raw { get; set; }
        public string? Imo { get; set; }
        public string? Callsign { get; set; }
        public string? Vessel_Name { get; set; }
        public string? Ship_Type { get; set; }
        public string? Cargo_Type { get; set; }
        public string? Width_Raw { get; set; }
        public string? Length_Raw { get; set; }
        public string? Position_Fixing_Device { get; set; }
        public string? Draught_Raw { get; set; }
        public string? Destination { get; set; }
        public string? Eta_Raw { get; set; }
        public string? Data_Source_Type { get; set; }
        public string? Size_A { get; set; }
        public string? Size_B { get; set; }
        public string? Size_C { get; set; }
        public string? Size_D { get; set; }
        //[Required]
        public string? Source_File_Name { get; set; }
        //[Required]
        public DateTime Source_Batch_Date { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }
}
