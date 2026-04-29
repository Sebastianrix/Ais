using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLayer.DTOs
{
    public class VoyageDTO
    {
        public int? Voyage_Id { get; set; }
        public int? Tanker_Id { get; set; }
        public string? Voyage_status { get; set; }
        public DateTime? Start_Time_Utc { get; set; }
        public DateTime? End_Time_Utc { get; set; }
        public int? Start_Position_Id { get; set; }
        public int? End_Position_Id { get; set; }
        public string? Start_Port_Name { get; set; }
        public string? End_Port_Name { get; set; }
        public string? Destination_Final { get; set; }
        public DateTime? Eta_final { get; set; }
        public DateTime Reated_At { get; set; }
        public DateTime Updated_At { get; set; }
    }
}
