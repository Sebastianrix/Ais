import { useState, useEffect} from 'react'
import { Button } from "@/components/ui/button";
import Navbar from '../components/Navbar'



import Menu from "@/components/Menu";
import styles from '../css/Overview.module.css';


function Overview(){
  return (
<>  

<Navbar></Navbar>
 
 
 
 <div className={styles.overviewHeader}> 
   <h1>System Overview</h1>

      <div className={styles.box}>
    <p><p>
The system processes AIS tanker data and presents it through an interactive map and web interface.
</p>
</p>
    </div>        

<div className={styles.cards}>
  <div className={styles.card}>
    <h3>Data Processing</h3>
    <p>Cleans and stores AIS tanker data.</p>
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

    <Menu>   
</Menu>


        </div>
</>
    )
}
export default Overview




