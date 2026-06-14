import { useState, useEffect} from 'react'

import styles from '../css/About.module.css';
import Navbar from '../components/Navbar'


function About(){
  
  return (
<>  
 <Navbar></Navbar>
    
 <div> <h1>About This Project</h1>
  <div className={styles.box}>

        
        <ul>
        <li>
        <p>
          This project is part of a Master's thesis in Computer Science at 
          Roskilde University.
        </p>
        </li>
        <li>
        <p>
          The system is developed by a team of two students and focuses on 
          analyzing maritime AIS data to explore patterns in vessel movements.
        </p>
        </li>
        <li>
        <p>
          Data is sourced from the Danish Maritime Authority:
          http://aisdata.ais.dk/
        </p>
        </li>
        <li>
        <p>
          The objective of the project is to investigate methods for detecting 
          anomalous or non-transparent shipping behavior using data-driven approaches.
        </p>
        </li>
        <li>
        <p>
          The platform is built using React, TypeScript, .NET, PostgreSQL, and Python.
        </p>
        </li>
        <li>
        <p>
          This project is intended for research and educational purposes only.
        </p>   
        </li>
        </ul>     
        </div>




{/* I moved the system overview here, since I wanted overview page to dispaly FETCHED API data instead*/}
 <h1>System Overview</h1>
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










        </div>

</>
    )
}
export default About