import { useState, useEffect } from 'react'
import { Card } from "@/components/ui/card";
import { Map, MapControls, MapMarker, MarkerContent, MarkerPopup } from "@/components/ui/map";
import styles from '../css/DisplayMap.module.css';
import Navbar from '../components/Navbar'
import { mapService } from "../services/MapService";
import { countryCodeToName} from "../components/helpers/countryCodes"


type Vessel = {
  tanker_Id: number; mmsi: string; vessel_Name: string; ship_Type: string;
  flag: string; latitude: number; longitude: number; timestamp_Utc: string;
  sog?: number; cog?: number; heading?: number; navigational_Status?: string;
  is_Anomalous: boolean; imo: string; callsign: string; cargo_Type: string;
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
        <Card className="p-0 rounded-none ring-0 border-0 w-full h-full overflow-hidden">
          <Map center={[11.0, 56.0]} zoom={6} loading={loading} theme="dark">
            <MapControls 
             position="bottom-left"
             showZoom={true}
             showCompass={true}  
             showLocate={false}
             showFullscreen={true}
            />
            {vessels.map(v => (
              <MapMarker key={v.tanker_Id} longitude={v.longitude} latitude={v.latitude}>
                <MarkerContent>
                  <div style={{ position: "relative", width: "28px", height: "28px" }}>

                  {/* Rotating triangle */}
                  <div style={{position: "absolute", top: 0, left: 0, transform: `rotate(${(!v.heading || v.heading === 511) ? (v.cog ?? 0) : v.heading}deg)`,transition: "transform 0.3s ease",}}>    
                    <svg width="28" height="28" viewBox="0 0 25 25" xmlns="http://www.w3.org/2000/svg">
                    <polygon points="12.5,0 20,25 5,25" 
                    fill="rgba(30, 41, 59, 0.5)"
                    stroke={v.is_Anomalous ? "#ef4444" : "#3b82f6"}
                    strokeWidth="1.5"
                    strokeLinejoin="round"
                    />
                   </svg>
                 </div>

                {/* Non-rotating text (flag) */}
                <div className={styles.flagText}>
               {v.flag}
                </div>
              </div>

              {/* The pop-up on click */}
              </MarkerContent>
                   <MarkerPopup className={styles.popUp}  closeButton>
              <strong>({v.flag}) {v.vessel_Name ?? "Unknown"}</strong>
              <br />
              Flag: {countryCodeToName[v.flag]}
              <br />
              longitude: {v.longitude}
              <br />
              latitude: {v.latitude}
              <br />
              Type: {v.ship_Type}
              <br />
              SOG: {v.sog}
              <br />
              Heading: {v.heading}
              <br />
              IMO: {v.imo}
              <br />
              MMSI: {v.heading}
              <br />
              callsign: {v.callsign}
              <br />
              cargo_Type: {v.cargo_Type}
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