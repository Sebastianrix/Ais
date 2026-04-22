import { useState, useEffect} from 'react'

import '../css/About.css';
import Navbar from '../components/Navbar'


function About(){
  
  return (
<>  
 <Navbar></Navbar>
     
 <div>
          <h1>About page</h1>
          <p>
           We are a small team of two people. Our data source is: http://aisdata.ais.dk/
          </p>
        </div>

</>
    )
}
export default About