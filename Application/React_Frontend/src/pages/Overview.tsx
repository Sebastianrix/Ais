import { useState, useEffect} from 'react'
import { Button } from "@/components/ui/button";
import Navbar from '../components/Navbar'



import Menu from "@/components/Menu";
import styles from '../css/Overview.module.css';


function Overview(){
  return (
<>  

<Navbar></Navbar>
 
 
 
 <div> 
   <h1>Browser-Tool</h1>
<Menu>
             
</Menu>
        </div>
</>
    )
}
export default Overview




