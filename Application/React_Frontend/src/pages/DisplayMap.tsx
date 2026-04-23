import { useState, useEffect} from 'react'
import { Button } from "@/components/ui/button";


//import Card from '@mui/material/Card'; // dependecy hell caused this. Proberbly should be removed, but this version of Card has more options, so maybe we will swap later
import { Card } from "@/components/ui/card";

import { Map, MapControls } from "@/components/ui/map";



import '../css/DisplayMap.css';
import Navbar from '../components/Navbar'

function DisplayMap(){
  return (
<>  

<Navbar></Navbar>
 <div className='myMapClass1'>
<Card className="p-0 rounded-none ring-0 border-0 w-[850px] h-[700px] overflow-hidden">
             
      <Map center={[11.000000,56.00000]} zoom={6} >
        
        <MapControls position="top-left"/>

  
  
      </Map>
     

    </Card>
        </div>
</>
    )
}
export default DisplayMap




