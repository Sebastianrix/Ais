import { useState, useEffect} from 'react'
import { Button } from "@/components/ui/button";

import { Map, MapControls } from "@/components/ui/map";
import { Card } from "@/components/ui/card";

import '../css/DisplayMap.css';
import Navbar from '../components/Navbar'

function DisplayMap(){
  return (
<>  

<Navbar></Navbar>
 <div className='myMapClass1'>
      
          <Card className="w-[850px] h-[700px] p-0 overflow-hidden">
             
      <Map center={[11.000000,56.00000]} zoom={6} >
        
        <MapControls position="top-left"/>

  
  
      </Map>
         <Button className='myButtonClass1'
>tools</Button>

    </Card>
        </div>
</>
    )
}
export default DisplayMap




