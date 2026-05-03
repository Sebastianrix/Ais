import { useState, useEffect} from 'react'
import { Button } from "@/components/ui/button";
import Navbar from '../components/Navbar'



import Menu from "@/components/Menu";
import '../css/Overview.css';


function Overview(){
  return (
<>  

<Navbar></Navbar>
 <div> <p>Text from page within the div?</p>
<Menu>
             
</Menu>
        </div>
</>
    )
}
export default Overview




