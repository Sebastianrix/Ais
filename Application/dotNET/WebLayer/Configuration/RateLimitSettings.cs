using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLayer.Configuration
{
    public class RateLimitSettings
    {
        public int PermitLimit { get; set; }

        public int WindowMinutes { get; set; }

        public int QueueLimit { get; set; }
    }
}
