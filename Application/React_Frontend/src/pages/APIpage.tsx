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
           <h4>
          OBS: we currently only allow tanker data. Meaning tanker-ships. This means the API does NOT return data on cargo-ships, fishing-vessels ETC.
          </h4>
          <p>
          We not only provide this platform, if you are a scientist or developer, we made our API freely available.
          </p>
          <p>Good Watch </p>

       
        </div>
<section id="left">
<div>
      <ul className="api-links"> 
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
