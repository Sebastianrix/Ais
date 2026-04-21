import { useState, useEffect} from 'react'
import Navbar from '../Components/Navbar';
import Footbar from '../Components/Footbar';
import '../css/APIpage.css';



function APIpage(){
  return (
<>  

 {/*<div>   </div>*/}
          <Navbar></Navbar>
    
     

 <div>
          <h1>API Page</h1>
          <p>
          Welcome to the API page. This is where you can find information about our API endpoints.
          </p>
        </div>
<section id="left">
<div>
  <ul>
<li>Base url : https://localhost:5001</li>
<li>/weatherforecast/</li>
<li>/swagger/</li>

</ul>
</div>
</section>

          <Footbar></Footbar>
</>
    )
}
export default APIpage