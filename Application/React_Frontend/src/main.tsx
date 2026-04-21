import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './css/index.css'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import 'bootstrap/dist/css/bootstrap.min.css';
import App from './App.tsx'
import About from './pages/About.tsx'
import FAQ from './pages/FAQ.tsx'
import APIpage from './pages/APIpage.tsx'
import Map from './pages/Map.tsx'

import Navbar from './Components/Navbar';
import Footbar from './Components/Footbar';

createRoot(document.getElementById('root')!).render(
//  <StrictMode>
  //  <App />
 // </StrictMode>,
 
<StrictMode>
      <div className="page-container">
    <div className="content-wrap">
          <Navbar></Navbar>
       
  <BrowserRouter>
    <Routes>  
      <Route path = "/" element={<App />} />
      <Route path = "/About" element={<About />} />
      <Route path = "/FAQ" element={<FAQ />} />
      <Route path = "/APIpage" element={<APIpage />} />
      <Route path = "/Map" element={<Map />} />
    </Routes>
  </BrowserRouter>
 
  </div>
   <Footbar></Footbar>
  </div>
</StrictMode>


,
)
