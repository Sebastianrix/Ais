import { useState, useEffect} from 'react'
import Navbar from '../Components/Navbar';
import '../css/APIpage.css';


function APIpage(){
  return (
<>  
 <div>
          <Navbar></Navbar>
        </div>
 <div>
          <h1>API Page</h1>
          <p>
          Welcome to the API page. This is where you can find information about our API endpoints.
          </p>
        </div>
</>
    )
}
export default APIpage