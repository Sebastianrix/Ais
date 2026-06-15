using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public interface IDataService
    {



        Task<List<VesselMapPosition>> GetLatestVesselPositionsAsync(int sinceHours);
 


        Task<PagedResult<TankerPosition>> GetTankerPositionsAsync(
            int page, 
            int pageSize,
            int? tankerId = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? imo = null);

        //   IList<TankerPosition> GetTankerPositionsByDateRange(DateTime startDate, DateTime endDate); //*Implement this
        //   IList<TankerPosition> GetTankerPositionByTanker(string tanker); //*Implement this
        Task<List<TankerPosition>> GetTankerPositionsSimpleAsync();

        Task<List<Tanker>> GetTankersSimpleAsync();

        Task<PagedResult<Tanker>> GetTankersAsync(
            int page,
            int pageSize,
            bool? isActive = null,
            string? imo = null,
            string? mmsi = null,
            string? search = null);

        Task<PagedResult<TankerStaging>> GetTankerStagingsAsync(
            int page, int pageSize,
            string? mmsi = null, string? imo = null,
            DateTime? startDate = null, DateTime? endDate = null);
        //IList<TankerStaging> GetTankerStagingByDateRange(DateTime startDate, DateTime endDate);
        //IList<TankerStaging> GetTankerStagingByTanker(string tanker);


        IList<TrackedTanker> GetTrackedTankers();

      //  IList<TankerVoyage> GetTankerVoyages(); Last table, we made python script that does some of this tech


        Stats GetStats();

        IList<DataConsumerQueue> GetDataConsumerQueue();
        IList<DataDateArchive> GetDataDateArchive();
        IList<AnomalyFlag> GetAnomalyFlags();
        IList<AnomalyType> GetAnomalyTypes();
        MmsiCountryCode? GetCountryByMmsi(string mmsi); // Lookup helper, maybe we don't need this, but here it is

        //IList<VesselMapPosition> GetLatestVesselPositions(
        //double? minLat, double? minLon, double? maxLat, double? maxLon,
        //int sinceHours, int cap);

    }
}
