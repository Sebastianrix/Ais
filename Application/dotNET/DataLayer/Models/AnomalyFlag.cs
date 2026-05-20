using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    internal class AnomalyFlag
    {
       public long Anomaly_Flag_Id { get; set; }
       public long? Tanker_Id { get; set; }
       public long? Position_Id { get; set; }
       public long? Staging_Id { get; set; }
       public long Anomaly_Type_Id { get; set; }
       public string Source { get; set; } = "system";
       public decimal? Confidence { get; set; }
       public string? Notes { get; set; }
       public DateTime? Created_At { get; set; }
    }
}
