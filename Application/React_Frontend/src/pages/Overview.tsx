import { useState, useEffect} from 'react'
import { Button } from "@/components/ui/button";


//import Card from '@mui/material/Card'; // dependecy hell caused this. Proberbly should be removed, but this version of Card has more options, so maybe we will swap later
import Menu from "@/components/Menu";




import '../css/Overview.css';
import Navbar from '../components/Navbar'

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




