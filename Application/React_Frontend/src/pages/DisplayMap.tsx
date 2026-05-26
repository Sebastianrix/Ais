import { useState, useEffect } from 'react'
import { Card } from "@/components/ui/card";
import { Map, MapControls, MapMarker, MarkerContent, MarkerPopup } from "@/components/ui/map";
import '../css/DisplayMap.css';
import Navbar from '../components/Navbar'

type Vessel = {
  tanker_Id: number; mmsi: string; vessel_Name: string; ship_Type: string;
  flag: string; latitude: number; longitude: number; timestamp_Utc: string;
  sog?: number; cog?: number; heading?: number; navigational_Status?: string;
  is_Anomalous: boolean;
};

function DisplayMap() {
  const [vessels, setVessels] = useState<Vessel[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch("https://api.aismap.dk/v1/Map")
      .then(res => res.json())
      .then((data: Vessel[]) => setVessels(data))
      .catch(err => console.error("Map fetch failed:", err))
      .finally(() => setLoading(false));
  }, []);

  return (
    <>
      <Navbar />
      <div className='myMapClass1'>
        <Card className="p-0 rounded-none ring-0 border-0 w-[850px] h-[700px] overflow-hidden">
          <Map center={[11.0, 56.0]} zoom={6} loading={loading}>
            <MapControls position="top-left" />
            {vessels.map(v => (
              <MapMarker key={v.tanker_Id} longitude={v.longitude} latitude={v.latitude}>
                <MarkerContent>
                  <div
                    className="h-3 w-3 rounded-full border-2 border-white shadow"
                    style={{ background: v.is_Anomalous ? "#ef4444" : "#3b82f6" }}
                  />
                </MarkerContent>
                <MarkerPopup closeButton>
                  <strong>{v.vessel_Name ?? "Unknown"}</strong><br />
                  Flag: {v.flag} · {v.ship_Type}<br />
                  SOG: {v.sog ?? "—"} kn · Heading: {v.heading ?? "—"}°<br />
                  {v.is_Anomalous && <span style={{color:"#ef4444"}}>⚠ Flagged (invalid MMSI)</span>}
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