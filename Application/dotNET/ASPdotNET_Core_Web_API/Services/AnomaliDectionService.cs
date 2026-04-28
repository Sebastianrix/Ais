/*
namespace ASP.NET_Core_Web_API.Services
{
    public class AnomaliDectionService
    {
        public VerifyMmsi(DTOs.RawMmsi) {
            if (int.TryParse(DTOs.RawMmsi)) {
                                             
                // Verified, should we transmute to Int?
                // OBS : we have this value "Anomaly_Flag"
                // we should proberbly examnd on this,
                // but be cursious of DATA SIZE!!
               DTOs.Anomaly_Flag = false;
        }
            else { 
            
              // Anomali! it's a string, we should log this 
            
            }
    }
}
    */