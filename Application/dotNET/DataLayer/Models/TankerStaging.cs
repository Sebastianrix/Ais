using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class TankerStaging
    {
        public int? Staging_Id { get; set; }
        public string? Timestamp_Raw { get; set; }
        public string? Type_Of_Mobile { get; set; }
        public string? Mmsi { get; set; }
        public string? Latitude_Raw { get; set; }
        public string? Longitude_Raw { get; set; }
        public string? Navigational_Status { get; set; }
        public string? Rot_Raw { get; set; }
        public string? Sog_Raw { get; set; }
        public string? Cog_Raw { get; set; }
        public string? Heading_Raw { get; set; }
        public string? Imo { get; set; }
        public string? Callsign { get; set; }
        public string? Vessel_Name { get; set; }
        public string? Ship_Type { get; set; }
        public string? Cargo_Type { get; set; }
        public string? Width_Raw { get; set; }
        public string? Length_Raw { get; set; }
        public string? Position_Fixing_Device { get; set; }
        public string? Draught_Raw { get; set; }
        public string? Destination { get; set; }
        public string? Eta_Raw { get; set; }
        public string? Data_Source_Type { get; set; }
        public int? Size_A { get; set; }
        public int? Size_B { get; set; }
        public int? Size_C { get; set; }
        public int? Size_D { get; set; }
     //   [Required]
        public string? Source_File_Name { get; set; }
      //  [Required]
        public DateTime? Source_Batch_Date { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}
//Table "public.tanker_staging"
//         Column         |            Type             | Collation | Nullable |                      Default
//------------------------+-----------------------------+-----------+----------+----------------------------------------------------
// staging_id             | bigint                      |           | not null | nextval('tanker_staging_staging_id_seq'::regclass)
// timestamp_raw          | character varying(100)      |           |          |
// type_of_mobile         | character varying(100)      |           |          |
// mmsi                   | character varying(20)       |           |          |
// latitude_raw           | character varying(50)       |           |          |
// longitude_raw          | character varying(50)       |           |          |
// navigational_status    | character varying(100)      |           |          |
// rot_raw                | character varying(50)       |           |          |
// sog_raw                | character varying(50)       |           |          |
// cog_raw                | character varying(50)       |           |          |
// heading_raw            | character varying(50)       |           |          |
// imo                    | character varying(20)       |           |          |
// callsign               | character varying(50)       |           |          |
// vessel_name            | character varying(255)      |           |          |
// ship_type              | character varying(100)      |           |          |
// cargo_type             | character varying(100)      |           |          |
// width_raw              | character varying(50)       |           |          |
// length_raw             | character varying(50)       |           |          |
// position_fixing_device | character varying(100)      |           |          |
// draught_raw            | character varying(50)       |           |          |
// destination            | character varying(255)      |           |          |
// eta_raw                | character varying(100)      |           |          |
// data_source_type       | character varying(50)       |           |          |
// size_a                 | numeric(10,2)               |           |          |
// size_b                 | numeric(10,2)               |           |          |
// size_c                 | numeric(10,2)               |           |          |
// size_d                 | numeric(10,2)               |           |          |
// source_file_name       | character varying(255)      |           |          |
// source_batch_date      | date                        |           |          |
// created_at             | timestamp without time zone |           |          | CURRENT_TIMESTAMP
// updated_at             | timestamp without time zone |           |          | CURRENT_TIMESTAMP
//Indexes:
//    "tanker_staging_pkey" PRIMARY KEY, btree (staging_id)
//Referenced by:
//    TABLE "tanker_pos