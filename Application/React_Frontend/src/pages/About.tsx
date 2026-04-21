import { useState, useEffect} from 'react'
import Navbar from '../Components/Navbar';
import '../css/About.css';
import Footbar from '../Components/Footbar';


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
               <Footbar></Footbar>
</>
    )
}
export default About