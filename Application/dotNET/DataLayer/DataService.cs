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

        
        public IList<Tanker> GetTankers() {
        return _context.Tankers
                .OrderByDescending(t => t.Last_Seen_At)
                .Take(10) // page here
                .ToList();
        }

        public IList<TankerStaging> GetTankerStagings() {
        return _context.TankerStagings
                .OrderByDescending(ts => ts.Timestamp_Raw)
                .Take(5) // Remove this after Paging, This hack>
                .ToList();
         }
	
	
        public IList<TrackedTanker> GetTrackedTankers() {
        return _context.TrackedTankers
                .OrderByDescending(tt => tt.Tracked_Id)
                .Take(10) // Remove this after Paging, This hack>
                .ToList();
        }
     //   public IList<Voyage> GetVoyages() {
       // return _context.Voyage.OrderByDescending(v.Voyage)
         //
       // return _context.TankerPositions
              //  .OrderByDescending(tp => tp.Timestamp)
          //      .Take(100) // Remove this after Paging, This hack>
            //    .ToList();
        //}      
    }
}
