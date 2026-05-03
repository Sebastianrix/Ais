import { useState, useEffect} from 'react'


import '../css/APIpage.css';
import Navbar from '@/components/Navbar';


function APIpage(){
  return (
<>  
<Navbar></Navbar>

     
<article >
<div className='box'>
 <ul>
  <li><h1>Public API</h1></li>
  <li><p>Welcome to the API references. This is where you can find information about our API endpoints.</p></li>
  <li><p>OBS: we currently only allow tanker data. Meaning tanker-ships. This means the API does NOT return data on cargo-ships, fishing-vessels ETC.</p></li>
  <li><p>We not only provide this platform, if you are a scientist or developer, we made our API freely available.</p></li>
  <li><p>Click here in the browser to test if you have connection to the API</p></li>
  <li><p>Good Watch </p></li>
  <li><p> Returns .JSON</p></li>
</ul>
</div>

<div className='box' >

      <ul className="api-links">
<div > 
<li><a href="https://api.aismap.dk/swagger">https://api.aismap.dk/swagger</a></li>
<li><a href="https://api.aismap.dk/api/v1/TankerPositions">https://api.aismap.dk/api/v1/TankerPositions</a></li>
<li><a href="https://api.aismap.dk/Tankers">https://api.aismap.dk/Tankers</a></li>
<li><a href="https://api.aismap.dk/TankerStaging">https://api.aismap.dk/TankerStaging</a></li>
<li><a href="https://api.aismap.dk/TrackedTanker">https://api.aismap.dk/TrackedTanker</a></li>

<li className='grey-text'>More coming..</li>
</div>
</ul>

</div>
</article>


</>
    )
}
export default APIpage
