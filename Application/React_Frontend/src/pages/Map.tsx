import { useState, useEffect} from 'react'
import Navbar from '../Components/Navbar';
import '../css/Map.css';


function Map(){
  
  return (
<>  
 <div>
          <Navbar></Navbar>
        </div>
 <div>
          <h1>Map page</h1>
          <p>
          This is surpose to display the map!
        </div>
</>
    )
}
export default Map