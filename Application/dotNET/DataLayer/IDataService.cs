using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IDataService
    {   // We need pagination so bad. Rn we are returning 1000 records, but it should be paged  



        IList<TankerPosition> GetTankerPositions();
        //   IList<TankerPosition> GetTankerPositionsByDateRange(DateTime startDate, DateTime endDate); //*Implement this
        //   IList<TankerPosition> GetTankerPositionByTanker(string tanker); //*Implement this


        IList<Tanker> GetTankers();

      //*note to self THIS ONE NEXT ::::  IList<TankerStaging> GetTankerStaging();
        //IList<TankerStaging> GetTankerStagingByDateRange(DateTime startDate, DateTime endDate);
        //IList<TankerStaging> GetTankerStagingByTanker(string tanker);


        IList<TrackedTanker> GetTrackedTankers();

      //  IList<TankerVoyage> GetTankerVoyages();

    }
}
