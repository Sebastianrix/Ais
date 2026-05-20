using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLayer.DTOs
{
    public class AnomalyType
    {
       public long Anomaly_Type_Id { get; set; }
       public string Code { get; set; } = string.Empty;
       public string Name { get; set; } = string.Empty;
       public string? Description { get; set; }
       public string Severity { get; set; } = string.Empty;
       public DateTime? Created_At { get; set; }
    }
}
