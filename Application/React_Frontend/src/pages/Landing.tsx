import { useState, useEffect} from 'react'
import Navbar from '../Components/Navbar';
import '../css/Landing.css';


function Landing(){
  return (
<>  
 <div>
          <Navbar></Navbar>
        </div>
 <div>
          <h1>Landing page</h1>
          <p>
          Welcome, this is where you land, I think we should change this page to somthing else. Anyhow, he's another page.
          </p>
        </div>
</>
    )
}
export default Landing