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
   
          <p>
          We not only provide this platform, if you are a scientist or developer, we made API freely available.
          </p>

       
        </div>
<section id="left">
<div>
  <ul>
<li><a href="https://api.aismap.dk/swagger">https://api.aismap.dk/swagger</a></li>
<li><a href="https://api.aismap.dk/api/v1/TankerPositions">https://api.aismap.dk/api/v1/TankerPositions</a></li>
<li>More coming..</li>


</ul>
</div>
</section>


</>
    )
}
export default APIpage
