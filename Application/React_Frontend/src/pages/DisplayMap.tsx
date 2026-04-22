import { useState, useEffect} from 'react'


import { Map, MapControls } from "@/components/ui/map";
import { Card } from "@/components/ui/card";

import '../css/DisplayMap.css';


function DisplayMap(){
  return (
<>  

 <div>
       
          <Card className="w-[1000px] h-[1000px] p-0 overflow-hidden">
      <Map center={[-74.006, 40.7128]} zoom={11}>
        <MapControls />
      </Map>
    </Card>
        </div>
</>
    )
}
export default DisplayMap




