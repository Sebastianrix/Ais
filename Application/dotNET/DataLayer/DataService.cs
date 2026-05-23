using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.AspNetCore.Http.Features;
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
   

        public async Task<PagedResult<TankerPosition>> GetTankerPositionsAsync(
            int page, int pageSize,
            int? tankerId = null, DateTime? startDate = null, DateTime? endDate = null)
            {

            var query = _context.TankerPositions.AsNoTracking();
            if (startDate == null) { startDate = DateTime.Now; } 
            if (tankerId.HasValue) query = query.Where(tp => tp.Tanker_Id == tankerId.Value);
            if (startDate.HasValue) query = query.Where(tp => tp.Timestamp >= startDate.Value);
            if (endDate.HasValue) query = query.Where(tp => tp.Timestamp <= endDate.Value);

            var total = await query.CountAsync();

            var items = await query.OrderByDescending(tp => tp.Timestamp).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResult<TankerPosition>
            {
                Items = items, Page = page, PageSize = pageSize, TotalItems = total
            };
        }


        public async Task<PagedResult<Tanker>> GetTankersAsync(int page, int pageSize, bool? isActive = null)
        {
            var query = _context.Tankers.AsNoTracking();

            if (isActive.HasValue) query = query.Where(t => t.Is_Active == isActive.Value);

            var total = await query.CountAsync();
            var items = await query.OrderByDescending(t => t.Last_Seen_At).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResult<Tanker>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalItems = total
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

