using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Tanker
    {
        public int? Tanker_Id { get; set; }
        public string? Imo {  get; set; }
        public string? Mmsi { get; set; }
        public string? Vessel_Name { get; set; }
        public string? Callsign { get; set; }
        public string? Ship_Type { get; set; }
        public string? Cargo_Type { get; set; }
        public string? Type_Of_Mobil { get; set; }
        public string? Width { get; set; }
        public string? Length{ get; set; }
        public string? Size_A{ get; set; }
        public string? Size_B { get; set; }
        public string? Size_C { get; set; }
        public string? Size_D { get; set; }
        public string? Flag{ get; set; }
        public DateTime? First_Seen_At { get; set; }
        public DateTime? Last_Seen_At { get; set; }
        public Boolean? Is_Active { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
}
}
