using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AisDB_Context : DbContext
    {
        public AisDB_Context(DbContextOptions<AisDB_Context> options) : base(options) { }

        public DbSet<TankerPosition> TankerPositions { get; set; }
        public DbSet<TankerStaging> TankerStagings { get; set; }
        public DbSet<Tanker> Tankers { get; set; }
        public DbSet<Voyage> Voyages { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapTankerPositions(modelBuilder);
            MapTankerStaging(modelBuilder);
            MapTankers(modelBuilder);
            MapTrackedTankers(modelBuilder);
            MapVoyages(modelBuilder);
        }
        // Map of 'tanker_positions' table
        // Map of 'tanker_staging' table
        // Map of 'tankers' table
        // Map of 'tracked_tankers' table
        // Map of 'voyages' table

        private static void MapTankerPositions(ModelBuilder modelBuilder)//  We have a Db with 5 tabels,
        { //23 columns
        modelBuilder.Entity<TankerPosition>().ToTable("tanker_positions");
        modelBuilder.Entity<TankerPosition>().HasKey(tp => tp.Position_Id);
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Position_Id).HasColumnName("position_id");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Tanker_Id).HasColumnName("tanker_id");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Voyage_Id).HasColumnName("voyage_id");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Staging_Id).HasColumnName("staging_id");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Timestamp).HasColumnName("timestamp_utc");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Latitude).HasColumnName("latitude");
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Longitude).HasColumnName("longitude");
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
        modelBuilder.Entity<TankerPosition>().Property(tp => tp.Created_At).HasColumnName("created_at");
        }
        private static void MapTankerStaging(ModelBuilder modelBuilder)
        { //31 columns
        modelBuilder.Entity<TankerStaging>().ToTable("tanker_staging");
        modelBuilder.Entity<TankerStaging>().HasKey(ts => ts.Staging_Id);
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Staging_Id).HasColumnName("staging_id");
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Timestamp_Raw).HasColumnName("timestamp_raw"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Type_Of_Mobile).HasColumnName("type_of_mobile"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Mmsi).HasColumnName("mmsi"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Latitude_Raw).HasColumnName("latitude_raw"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Longitude_Raw).HasColumnName("longitude_raw"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Navigational_Status).HasColumnName("navigational_status"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Rot_Raw).HasColumnName("rot_raw"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Cog_Raw).HasColumnName("sog_raw"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Cog_Raw).HasColumnName("cog_raw"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Heading_Raw).HasColumnName("heading_raw"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Imo).HasColumnName("imo"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Callsign).HasColumnName("callsign"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Vessel_Name).HasColumnName("vessel_name"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Ship_Type).HasColumnName("ship_type"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Cargo_Type).HasColumnName("cargo_type"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Width_Raw).HasColumnName("width_raw"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Length_Raw).HasColumnName("length_raw"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Position_Fixing_Device).HasColumnName("position_fixing_device"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Draught_Raw).HasColumnName("draught_raw"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Destination).HasColumnName("destination"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Eta_Raw).HasColumnName("eta_raw"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Data_Source_Type).HasColumnName("data_source_type"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Size_A).HasColumnName("size_a"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Size_B).HasColumnName("size_b"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Size_C).HasColumnName("size_c"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Size_D).HasColumnName("size_d"); ; 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Source_File_Name).HasColumnName("source_file_name"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Source_Batch_Date).HasColumnName("source_batch_date"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Created_At).HasColumnName("created_at"); 
        modelBuilder.Entity<TankerStaging>().Property(ts => ts.Updated_At).HasColumnName("updated_at"); 
        }
        private static void MapTankers(ModelBuilder modelBuilder)
        {// 20 columns
        modelBuilder.Entity<Tanker>().ToTable("tankers");
        modelBuilder.Entity<Tanker>().HasKey(t=> t.Tanker_Id);
        modelBuilder.Entity<Tanker>().Property(t => t.Tanker_Id).HasColumnName("tanker_id");
        modelBuilder.Entity<Tanker>().Property(t => t.Imo).HasColumnName("imo"); 
        modelBuilder.Entity<Tanker>().Property(t => t.Mmsi).HasColumnName("mmsi"); 
        modelBuilder.Entity<Tanker>().Property(t => t.Vessel_Name).HasColumnName("vessel_name"); 
        modelBuilder.Entity<Tanker>().Property(t => t.Callsign).HasColumnName("callsign"); 
        modelBuilder.Entity<Tanker>().Property(t => t.Ship_Type).HasColumnName("ship_type"); 
        modelBuilder.Entity<Tanker>().Property(t => t.Cargo_Type).HasColumnName("cargo_type"); 
        modelBuilder.Entity<Tanker>().Property(t => t.Type_Of_Mobil).HasColumnName("type_of_mobile"); 
        modelBuilder.Entity<Tanker>().Property(t => t.Width).HasColumnName("width"); 
        modelBuilder.Entity<Tanker>().Property(t => t.Length).HasColumnName("length"); 
        modelBuilder.Entity<Tanker>().Property(t => t.Size_A).HasColumnName("size_a"); 
        modelBuilder.Entity<Tanker>().Property(t => t.Size_B).HasColumnName("size_b"); 
        modelBuilder.Entity<Tanker>().Property(t => t.Size_C).HasColumnName("size_c"); 
        modelBuilder.Entity<Tanker>().Property(t => t.Size_D).HasColumnName("size_d"); 
        modelBuilder.Entity<Tanker>().Property(t => t.Flag).HasColumnName("flag"); 
        modelBuilder.Entity<Tanker>().Property(t => t.First_Seen_At).HasColumnName("first_seen_at"); 
        modelBuilder.Entity<Tanker>().Property(t => t.Last_Seen_At).HasColumnName("last_seen_at"); 
        modelBuilder.Entity<Tanker>().Property(t => t.Is_Active).HasColumnName("is_active"); 
        modelBuilder.Entity<Tanker>().Property(t => t.Created_At).HasColumnName("created_at"); 
        modelBuilder.Entity<Tanker>().Property(t => t.Updated_At).HasColumnName("updated_at"); 
        }
        private static void MapTrackedTankers(ModelBuilder modelBuilder)
        { // 7 columns 
        modelBuilder.Entity<TrackedTanker>().ToTable("tracked_tankers");
        modelBuilder.Entity<TrackedTanker>().HasKey(tt=> tt.Tracked_Id);
        modelBuilder.Entity<TrackedTanker>().Property(tt => tt.Tracked_Id).HasColumnName("tracked_id");
        modelBuilder.Entity<TrackedTanker>().Property(tt => tt.Imo).HasColumnName("imo"); 
        modelBuilder.Entity<TrackedTanker>().Property(tt => tt.Source_Trial).HasColumnName("source_trial"); 
        modelBuilder.Entity<TrackedTanker>().Property(tt => tt.Notes).HasColumnName("notes"); 
        modelBuilder.Entity<TrackedTanker>().Property(tt => tt.Is_Active).HasColumnName("is_active"); 
        modelBuilder.Entity<TrackedTanker>().Property(tt => tt.Created_At).HasColumnName("created_at"); 
        modelBuilder.Entity<TrackedTanker>().Property(tt => tt.Updated_At).HasColumnName("updated_at"); 
        }
        private static void MapVoyages(ModelBuilder modelBuilder)
        {//  13 columns
        modelBuilder.Entity<Voyage>().ToTable("voyages");
        modelBuilder.Entity<Voyage>().HasKey(v=> v.Voyage_Id);
        modelBuilder.Entity<Voyage>().Property(v => v.Tanker_Id).HasColumnName("voyage_id");
        modelBuilder.Entity<Voyage>().Property(v => v.Imo).HasColumnName("tanker_id"); 
        modelBuilder.Entity<Voyage>().Property(v => v.Mmsi).HasColumnName("voyage_status"); 
        modelBuilder.Entity<Voyage>().Property(v => v.Vessel_Name).HasColumnName("start_time_utc"); 
        modelBuilder.Entity<Voyage>().Property(v => v.Callsign).HasColumnName("end_time_utc"); 
        modelBuilder.Entity<Voyage>().Property(v => v.Ship_Type).HasColumnName("start_position_id"); 
        modelBuilder.Entity<Voyage>().Property(v => v.End_Position_Id).HasColumnName("end_position_id"); 
        modelBuilder.Entity<Voyage>().Property(v => v.Start_Port_Name).HasColumnName("start_port_name"); 
        modelBuilder.Entity<Voyage>().Property(v => v.End_Port_Name).HasColumnName("end_port_name"); 
        modelBuilder.Entity<Voyage>().Property(v => v.Destination_Final).HasColumnName("destination_final"); 
        modelBuilder.Entity<Voyage>().Property(v => v.Eta_final).HasColumnName("eta_final"); 
        modelBuilder.Entity<Voyage>().Property(v => v.Reated_At).HasColumnName("created_at"); 
        modelBuilder.Entity<Voyage>().Property(v => v.Updated_At).HasColumnName("updated_at"); 
        }
    }
}


//  SELFGUIDE : 
//  To implement a new endpoint using DataBase

//  Make functions in DataService/IDataService
//  and create a new Controller.cs
//  
/*
      IDataService/DataService?
      Controllers

      (https test in WepAPIhttp)
      Unit-Test ( should we make these? )

Frontend ->
       /types ( DTO.ts)
 */
// We should properbly mention this in the report, these's tons of seperation of Layers here. 


