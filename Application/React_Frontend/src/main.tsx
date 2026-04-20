import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './css/index.css'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import App from './App.tsx'
import About from './pages/About.tsx'
import FAQ from './pages/FAQ.tsx'
import Landing from './pages/Landing.tsx'
import Map from './pages/Map.tsx'


createRoot(document.getElementById('root')!).render(
//  <StrictMode>
  //  <App />
 // </StrictMode>,

<StrictMode>
  <BrowserRouter>
    <Routes>  
      <Route path = "/" element={<App />} />
      <Route path = "/About" element={<About />} />
      <Route path = "/FAQ" element={<FAQ />} />
      <Route path = "/Landing" element={<Landing />} />
      <Route path = "/Map" element={<Map />} />
    </Routes>
  </BrowserRouter>
</StrictMode>


,
)
