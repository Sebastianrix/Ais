using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AisDB_Context : DbContext
    {
        public AisDB_Context(DbContextOptions<AisDB_Context> options) : base(options) { }

        // As my once good friend once said: sanity check - We have a Db with 5 tabels,
        // so lets maybe start with those, then scale more.
        // Bookmarks for certain interest would be awesome, but moves into realm of user framwork stuff
        // Which we know how to implement with JWT and such. But lets focus on the Psql made for now.
        // I imagine we can have end points for get/vessels, this could be for a overview display page. 

        // Map of 'tanker_positions' table
        // Map of 'tanker_staging' table
        // Map of 'tankers' table
        // Map of 'tracked_tankers' table
        // Map of 'voyages' table

        // Implementation reminder* Put Model for object in the datalayer /Models folder -> MapSQL -> IDataService/DataService? -> WebAPI DTO -> types/ .TS DTO -> Done
        // We should properbly mention this in the report, these's tons of seperation of Layers here. 



        public DbSet<TankerPosition> TankerPositions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapTankerPositions(modelBuilder);
            MapTankerStaging(modelBuilder);
            MapTankers(modelBuilder);
            MapTrackedTankers(modelBuilder);
            MapVoyages(modelBuilder);
        }

        private static void MapTankerPositions(ModelBuilder modelBuilder){

        }
        private static void MapTankerStaging(ModelBuilder modelBuilder){}
        private static void MapTankers(ModelBuilder modelBuilder){}
        private static void MapTrackedTankers(ModelBuilder modelBuilder){}
        private static void MapVoyages(ModelBuilder modelBuilder){}





    }
}

