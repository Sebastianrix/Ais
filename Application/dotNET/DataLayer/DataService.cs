using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataService : IDataService
    {
        private readonly AisDB_Context _context;

        public DataService(AisDB_Context context)
        {
            _context = context;
        }


        public async Task<List<VesselMapPosition>> GetLatestVesselPositionsAsync(int sinceHours = 168)
        {
            // Latest position per tanker. DISTINCT ON is PSQL only,. Meaning we need raw SQL.
            // Of course this break some patterns, but it was the most practical solution since there is a capability gap betwen LINQ and PSQL.
            // If we really wanted to stick with LINQ, we could have used GroupBy().Select(First()), which would be more fragile,so we choose raw SQL since we really needed that DISTINCT.
            var sql = @"
        SELECT t.tanker_id           AS tanker_id,
       t.mmsi                AS mmsi,
       t.vessel_name         AS vessel_name,
       t.ship_type           AS ship_type,
       t.flag                AS flag,
       p.latitude            AS latitude,
       p.longitude           AS longitude,
       p.timestamp_utc       AS timestamp_utc,
       p.sog                 AS sog,
       p.cog                 AS cog,
       p.heading             AS heading,
       p.navigational_status AS navigational_status,
       p.is_anomalous        AS is_anomalous
FROM tankers t
CROSS JOIN LATERAL (
    SELECT p.latitude, p.longitude, p.timestamp_utc,
           p.sog, p.cog, p.heading, p.navigational_status,
           COALESCE(p.anomaly_flag, FALSE) AS is_anomalous
    FROM tanker_positions p
    WHERE p.tanker_id = t.tanker_id
    ORDER BY p.timestamp_utc DESC
    LIMIT 1
) p
WHERE t.is_active = TRUE";

            return await _context.VesselMapPositions
                .FromSqlRaw(sql)
                .AsNoTracking()
                .ToListAsync();


        }

        public async Task<PagedResult<TankerPosition>> GetTankerPositionsAsync(
            int page, int pageSize,
            int? tankerId = null, DateTime? startDate = null, DateTime? endDate = null, string? imo = null)
            {

            var query = _context.TankerPositions.AsNoTracking();


            if (!string.IsNullOrWhiteSpace(imo)) 
            { 
                var matchedTankerId = await _context.Tankers.Where(t => t.Imo == imo).Select(t => (long?)t.Tanker_Id).FirstOrDefaultAsync();
                if (matchedTankerId == null) return new PagedResult<TankerPosition> { Items = new List<TankerPosition>(), Page = page, PageSize = pageSize, TotalItems = 0 };
                
              query = query.Where(tp => tp.Tanker_Id == matchedTankerId.Value);
            }
       
            if (tankerId.HasValue) query = query.Where(tp => tp.Tanker_Id == tankerId.Value);
            if (startDate.HasValue) query = query.Where(tp => tp.Timestamp >= DateTime.SpecifyKind(startDate.Value, DateTimeKind.Utc));
            if (endDate.HasValue) query = query.Where(tp => tp.Timestamp <= DateTime.SpecifyKind(endDate.Value, DateTimeKind.Utc));

          //  var total = await query.CountAsync(); // This line broke the API, for milions of row, it's expensive. Maybe Index will fix
           // regardless it's descarded.

            var items = await query.OrderByDescending(tp => tp.Timestamp).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResult<TankerPosition>{Items = items, Page = page, PageSize = pageSize, TotalItems = -1}; // -1 means, do not compute (this was nesssesary becausee of our huge tablesize)
        }

        public async Task<List<TankerPosition>> GetTankerPositionsSimpleAsync()
            {return await _context.TankerPositions.AsNoTracking().OrderByDescending(tp => tp.Timestamp).Take(50).ToListAsync(); }



        public async Task<List<Tanker>> GetTankersSimpleAsync()
        {
            return await _context.Tankers
                .AsNoTracking()
                .OrderByDescending(t => t.Last_Seen_At)
                .Take(50)
                .ToListAsync();
        }

        public async Task<PagedResult<Tanker>> GetTankersAsync(int page, int pageSize, bool? isActive = null, string? imo = null, string? mmsi = null, string? search = null)
        {
            var query = _context.Tankers.AsNoTracking();

            if (isActive.HasValue) query = query.Where(t => t.Is_Active == isActive.Value);

            if (!string.IsNullOrWhiteSpace(imo)) query = query.Where(t => t.Imo == imo);

            if (!string.IsNullOrWhiteSpace(mmsi)) query = query.Where(t => t.Mmsi == mmsi);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim();
                // This line below looks add, but it's the builin case-insensitivity for Npgsql. 
               // Makes good sense if the user SEARCH a vessel name, capital or lowercase shouldnt matter.
                query = query.Where(t => EF.Functions.ILike(t.Vessel_Name, $"%{s}%") || t.Imo == s || t.Mmsi == s);
            }
            var total = await query.CountAsync();
            var items = await query.OrderByDescending(t => t.Last_Seen_At).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResult<Tanker>
            {
                Items = items,Page = page, PageSize = pageSize, TotalItems = total
            };
        }

        public async Task<PagedResult<TankerStaging>> GetTankerStagingsAsync(
            int page, int pageSize,
            string? mmsi = null, string? imo = null,
            DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.TankerStagings.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(mmsi))
                query = query.Where(s => s.Mmsi == mmsi);
            if (!string.IsNullOrWhiteSpace(imo))
                query = query.Where(s => s.Imo == imo);
            if (startDate.HasValue)
                query = query.Where(s => s.Source_Batch_Date >= startDate.Value);
            if (endDate.HasValue)
                query = query.Where(s => s.Source_Batch_Date <= endDate.Value);

            bool hasFilter = !string.IsNullOrWhiteSpace(mmsi)|| !string.IsNullOrWhiteSpace(imo)||startDate.HasValue||endDate.HasValue;

            long totalItems;
            if (hasFilter)
            {
                totalItems = await query.CountAsync();
            }
            else
            {
                totalItems = await _context.Database
                    .SqlQueryRaw<long>(
                        "SELECT reltuples::bigint AS \"Value\" FROM pg_class WHERE relname = 'tanker_staging'")
                    .SingleAsync();
            }

            var items = await query
                .OrderByDescending(s => s.Staging_Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            return new PagedResult<TankerStaging>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                Items = items
            };
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

        public Stats GetStats()
        {
            var archive = _context.DataDateArchive.AsNoTracking();
            var queue = _context.DataConsumerQueue.AsNoTracking();
            return new Stats
            {   
                // Small tables (we can count)
                TankerCount = _context.Tankers.Count(),
                TrackedTankerCount = _context.TrackedTankers.Count(),
                // Big tables (we can't do count, so we use the noted counts from archive)
                TotalPositionsProcessed = archive.Sum(a => (long?)a.Positions_Inserted) ?? 0,
                TotalStagingRowsProcessed = archive.Sum(a => (long?)a.Total_Rows) ?? 0,

                DatesProcessed = archive.Count(),

                LatestBatchDate = archive.Max(a => (DateTime?)a.Source_Batch_Date),
                OldestBatchDate = archive.Min(a => (DateTime?)a.Source_Batch_Date),
                // Queue status
                PendingBatches = queue.Count(q => q.Status == "pending"),
                InProgressBatches = queue.Count(q => q.Status == "in_progress")
            };
        }

        public IList<DataConsumerQueue> GetDataConsumerQueue()
        {
            return _context.DataConsumerQueue
                .Where(q => q.Status == "pending" || q.Status == "in_progress")
                .OrderBy(q => q.Priority)
                .ThenBy(q => q.Source_Batch_Date)

                .ToList();
        }

        public IList<DataDateArchive> GetDataDateArchive()
        {
            return _context.DataDateArchive
                .OrderByDescending(a => a.Source_Batch_Date)
                .ToList();
        }

        public IList<AnomalyFlag> GetAnomalyFlags()
        {
            return _context.AnomalyFlags
                .OrderByDescending(af => af.Created_At)
    
                .ToList();
        }

        // Anomaly types (small static table)
        public IList<AnomalyType> GetAnomalyTypes()
        {
            return _context.AnomalyTypes
                .OrderBy(at => at.Severity)
                .ThenBy(at => at.Name)
                .ToList();
        }

        // MMSI country lookup helper
        public MmsiCountryCode? GetCountryByMmsi(string mmsi)
        {
            if (string.IsNullOrEmpty(mmsi) || mmsi.Length < 3)
                return null;

            var mid = mmsi.Substring(0, 3);
            return _context.MmsiCountryCodes
                .FirstOrDefault(mcc => mcc.Mid_Code == mid);
        }

        


    }
}

