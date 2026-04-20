import { useState, useEffect} from 'react'
import Navbar from '../Components/Navbar';
import '../css/About.css';


function About(){
  
  return (
<>  
 <div>
          <Navbar></Navbar>
        </div>
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