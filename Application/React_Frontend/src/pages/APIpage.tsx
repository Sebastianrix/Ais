import { useState, useEffect} from 'react'


import '../css/APIpage.css';
import Navbar from '@/components/Navbar';


function APIpage(){
  return (
<>  
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


</>
    )
}
export default APIpage