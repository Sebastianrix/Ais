using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Tanker
    {
        public int? Tanker_Id { get; set; }
        public string? Imo {  get; set; }
        public string? Mmsi { get; set; }
        public string? Vessel_Name { get; set; }
        public string? Callsign { get; set; }
        public string? Ship_Type { get; set; }
        public string? Cargo_Type { get; set; }
        public string? Type_Of_Mobil { get; set; }
        public int?    Width { get; set; }
        public int?    Length { get; set; }
        public int?    Size_A { get; set; }
        public int?    Size_B { get; set; }
        public int?    Size_C { get; set; }
        public int?    Size_D { get; set; }
        public string? Flag{ get; set; }
        public DateTime? First_Seen_At { get; set; }
        public DateTime? Last_Seen_At { get; set; }
        public Boolean? Is_Active { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
}
}
//Table "public.tankers"
//     Column     |            Type             | Collation | Nullable |                  Default
//----------------+-----------------------------+-----------+----------+--------------------------------------------
// tanker_id      | bigint                      |           | not null | nextval('tankers_tanker_id_seq'::regclass)
// imo            | character varying(20)       |           |          |
// mmsi           | character varying(20)       |           |          |
// vessel_name    | character varying(255)      |           |          |
// callsign       | character varying(50)       |           |          |
// ship_type      | character varying(100)      |           |          |
// cargo_type     | character varying(100)      |           |          |
// type_of_mobile | character varying(100)      |           |          |
// width          | numeric(10,2)               |           |          |
// length         | numeric(10,2)               |           |          |
// size_a         | numeric(10,2)               |           |          |
// size_b         | numeric(10,2)               |           |          |
// size_c         | numeric(10,2)               |           |          |
// size_d         | numeric(10,2)               |           |          |
// flag           | character varying(100)      |           |          |
// first_seen_at  | timestamp without time zone |           |          |
// last_seen_at   | timestamp without time zone |           |          |
// is_active      | boolean                     |           |          | true
// created_at     | timestamp without time zone |           |          | CURRENT_TIMESTAMP
// updated_at     | timestamp without time zone |           |          | CURRENT_TIMESTAMP
//Indexes:
//    "tankers_pkey" PRIMARY KEY, btree (tanker_id)
//    "idx_tankers_imo" btree (imo)
//    "idx_tankers_mmsi" btree (mmsi)
//    "uq_tankers_imo" UNIQUE CONSTRAINT, btree (imo)
//Referenced by:
//    TABLE "tanker_positions" CONSTRAINT "fk_positions_tanker" FOREIGN KEY (tanker_id) REFERENCES tankers(tanker_id) ON DELETE CASCADE
//    TABLE "voyages" CONSTRAINT "fk_voyages_tanker" FOREIGN KEY (tanker_id) REFERENCES tankers(tanker_id) ON DELETE CASCADE