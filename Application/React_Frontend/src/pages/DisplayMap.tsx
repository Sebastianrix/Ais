import { useState, useEffect } from 'react'
import { Card } from "@/components/ui/card";
import { Map, MapControls, MapMarker, MarkerContent, MarkerPopup } from "@/components/ui/map";
import styles from '../css/DisplayMap.module.css';
import Navbar from '../components/Navbar'
import { mapService } from "../services/MapService";



type Vessel = {
  tanker_Id: number; mmsi: string; vessel_Name: string; ship_Type: string;
  flag: string; latitude: number; longitude: number; timestamp_Utc: string;
  sog?: number; cog?: number; heading?: number; navigational_Status?: string;
  is_Anomalous: boolean;
};








function DisplayMap() {
  const [vessels, setVessels] = useState<Vessel[]>([]);
  const [loading, setLoading] = useState<boolean>(true);


useEffect(() => {
  mapService.get(168)
    .then(setVessels)
    .catch(err => console.error("Map fetch failed:", err))
    .finally(() => setLoading(false));
}, []);

  return (
    <>
      <Navbar />
      <div className={styles.mapContainer}>
        <Card className="p-0 rounded-none ring-0 border-0 w-[850px] h-[700px] overflow-hidden">
          <Map center={[11.0, 56.0]} zoom={6} loading={loading}>
            <MapControls position="top-left" />
            {vessels.map(v => (
              <MapMarker key={v.tanker_Id} longitude={v.longitude} latitude={v.latitude}>
                <MarkerContent>
                  <div className={styles.markerCircle}
    style={{
      width: "22px",
      height: "22px",
      borderColor: v.is_Anomalous ? "#ef4444" : "#3b82f6",
      fontSize: "12px",
    }}
  >
    {v.flag}
  </div>
      </MarkerContent>
          <MarkerPopup className={styles.popUp}  closeButton>
              <strong>({v.flag}) {v.vessel_Name ?? "Unknown"}</strong>
              <br />
              Flag: (v.flag) {v.flag}
              <br />
              Type: {v.ship_Type}
              <br />
              SOG: {v.sog ?? "—"} kn 
              <br />
              Heading: {v.heading ?? "—"}°
              <br />
              Date: {v.timestamp_Utc.split("T")[0]}
              <br />
              Hour: {v.timestamp_Utc.split("T")[1]}
              {v.is_Anomalous && (
              <span className={styles.anomalyText}>
              Flagged (invalid MMSI)
              </span>
              )}
              </MarkerPopup>
          </MapMarker>
              ))}
          </Map>
        </Card>
      </div>
    </>
  );
}
export default DisplayMap;