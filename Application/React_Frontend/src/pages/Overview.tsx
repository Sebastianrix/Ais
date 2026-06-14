import { useState, useEffect} from 'react'

import Navbar from '../components/Navbar'



import Menu from "@/components/Menu";
import styles from '../css/Overview.module.css';


import { statsService } from "../services/statsService";
import type { Stats } from "../types/Stats";




function Overview(){


  const [stats, setStats] = useState<Stats | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const loadStats = async () => {
      try {
        const data = await statsService.get();
        setStats(data);
       
      } catch (err) {
        console.error("Failed to load stats", err);
      } finally {
        setLoading(false);
      }
       console.log("Hey look here: "+stats);
    };

    loadStats();
  }, []);

  

  return (
<>  

<Navbar></Navbar>
 
 
 
 <div className={styles.overviewHeader}> 
   <h1>Data Overview</h1>

    <div >
        <p>
          This pages allows you to browse and search data.
        </p>
    </div>        



<div className={styles.cards}>
  <div className={styles.card}>
      <h3>Tankers</h3>
      {loading ? (<p>Loading...</p>) : (<p>{stats?.tankerCount.toLocaleString()} tankers found</p>)}
  </div>
  <div className={styles.card}>
    <h3>Interactive Map</h3>
    <p>Displays vessel positions on an interactive map.</p>
  </div>

  <div className={styles.card}>
    <h3>Anomaly Detection</h3>
    <p>Identifies suspicious AIS records.</p>
  </div>

  <div className={styles.card}>
    <h3>REST API</h3>
    <p>Provides access to processed data.</p>
  </div>
</div>

    <Menu >   
</Menu>


        </div>
</>
    )
}
export default Overview




