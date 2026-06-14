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
  {/*This card shows total tankers found */}
  <div className={styles.card}>
      <h3>Tankers</h3>
      {loading ? (<p>Loading...</p>) : (
        <>
      <p>{stats?.tankerCount.toLocaleString()} tankers found</p>
      <p>{stats?.totalPositionsProcessed.toLocaleString()} tanker positions</p>
        </>
        )}
  </div>

  {/*This card shows total tankers positions */}
  <div className={styles.card}>
      <h3>Tankers</h3>
      {loading ? (<p>Loading...</p>) : (<p>{stats?.tankerCount.toLocaleString()} tankers found</p>)}
  </div>

  {/*This card shows total tankers positions */}
  <div className={styles.card}>
      <h3>Tankers</h3>
      {loading ? (<p>Loading...</p>) : (<p>{stats?.tankerCount.toLocaleString()} tankers found</p>)}
  </div>


  {/*This card shows total tankers positions */}
  <div className={styles.card}>
      <h3>Tankers</h3>
      {loading ? (<p>Loading...</p>) : (<p>{stats?.tankerCount.toLocaleString()} tankers found</p>)}
  </div>
</div>

    <Menu >   
</Menu>


        </div>
</>
    )
}
export default Overview




