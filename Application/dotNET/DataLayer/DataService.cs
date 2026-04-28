using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataLayer
{
    public class DataService : IDataService
    {
        private readonly AisDB_Context _context;

        public DataService(IConfiguration configuration)
        {
            var options = new DbContextOptionsBuilder<AisDB_Context>().UseNpgsql(configuration.GetConnectionString("aisDatabase")).Options;
            _context = new AisDB_Context(options);
        }

        // private IList<T> GetPagedResults   *Implement later*

        public IList<TankerPosition> GetTankerPositions() {
        return _context.TankerPositions
                .OrderByDescending(tp => tp.Timestamp)
                .Take(100) // Remove this after Paging, This hack just make the program not freeze, since otherwise SQL would return thusands of rows.
                .ToList();
        }
      
    }
}
