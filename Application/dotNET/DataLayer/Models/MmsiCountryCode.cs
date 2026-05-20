using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    internal class MmsiCountryCode
    {
        public string Mid_Code { get; set; } = string.Empty;
        public string Country_Code { get; set; } = string.Empty;
        public string Country_Name { get; set; } = string.Empty;
        public string? Region { get; set; }
        public DateTime? Created_At { get; set; }
    }
}
