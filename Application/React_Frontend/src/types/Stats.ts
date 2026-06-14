export interface Stats {
  tankerCount: number;
  trackedTankerCount: number;

  totalPositionsProcessed: number;
  totalStagingRowsProcessed: number;
  datesProcessed: number;

  latestBatchDate: string;
  oldestBatchDate: string;

  pendingBatches: string;
  inProgressBatches: string;
}

       // public int TankerCount { get; set; }
       // public int TrackedTankerCount { get; set; }


       // public long TotalPositionsProcessed { get; set; }
      //  public long TotalStagingRowsProcessed { get; set; }
       // public int DatesProcessed { get; set; }
      //  public DateTime? LatestBatchDate { get; set; }
     //   public DateTime? OldestBatchDate { get; set; }


   //     public int PendingBatches { get; set; }
   //     public int InProgressBatches { get; set; }