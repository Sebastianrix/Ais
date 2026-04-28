using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        /*

        TO-DO list / Work-flow

         C# -> 
              /Models folder ( objects.cs  )
              /WebAPI DTO ( objectDTO.cs )
              MapSQL in DbContext.cs

              IDataService/DataService?
              Controllers
              
              (https test in WepAPIhttp)
              Unit-Test 
        
        Frontend ->
               /types ( DTO.ts)
         */
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

        private static void MapTankerPositions(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<TankerPosition>().ToTable("tanker_positions");
        modelBuilder.Entity<TankerPosition>().HasKey(tp => tp.Position_Id);
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Position_Id).HasColumnName("position_id");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Tanker_Id).HasColumnName("tanker_id");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Voyage_Id).HasColumnName("voyage_id");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Staging_Id).HasColumnName("staging_id");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Timestamp).HasColumnName("timestamp");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Lantitude).HasColumnName("lantitude");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Latitude).HasColumnName("latitude");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Raw_Imo).HasColumnName("raw_imo");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Imo_Status).HasColumnName("imo_status");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Raw_Mmsi).HasColumnName("raw_mmsi");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Mmsi_Status).HasColumnName("mmsi_status");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Anomaly_Flag).HasColumnName("anomaly_flag");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Navigational_Status).HasColumnName("navigational_status");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Rot).HasColumnName("rot");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Sog).HasColumnName("sog");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Cog).HasColumnName("cog");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Heading).HasColumnName("heading");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Draught).HasColumnName("draught");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Destination).HasColumnName("destination");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Eta).HasColumnName("eta");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Position_Fixing_Device).HasColumnName("position_fixing_device");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Data_Source_Type).HasColumnName("data_source_type");
        
        
        }
        
        
        private static void MapTankerStaging(ModelBuilder modelBuilder){}
        private static void MapTankers(ModelBuilder modelBuilder){}
        private static void MapTrackedTankers(ModelBuilder modelBuilder){}
        private static void MapVoyages(ModelBuilder modelBuilder){}





    }
}

