import { useState, useEffect} from 'react'
import Navbar from '../Components/Navbar';
import Footbar from '../Components/Footbar';
import '../css/FAQ.css';


function FAQ(){
  
  return (
<>  
 
          <Navbar></Navbar>
       
 <div>
          <h1>FAQ page</h1>
          <p>
           Questions and answers list coming soon
          </p>
        </div>
            <Footbar></Footbar>
</>
    )
}
export default FAQ