import { useState, useEffect} from 'react'


import { Map, MapControls } from "@/components/ui/map";
import { Card } from "@/components/ui/card";

import '../css/DisplayMap.css';


function DisplayMap(){
  return (
<>  

 <div className='myMapClass1'>
       
          <Card className="w-[850px] h-[700px] p-0 overflow-hidden">
      <Map center={[11.000000,56.00000]} zoom={6} >
        <MapControls />
      </Map>
    </Card>
        </div>
</>
    )
}
export default DisplayMap




