import { useState, useEffect} from 'react'

import '../css/About.css';
import Navbar from '../components/Navbar'


function About(){
  
  return (
<>  
 <Navbar></Navbar>
     
 <div>
        <h1>About This Project</h1>
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

</>
    )
}
export default About