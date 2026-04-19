import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import About from './pages/About.tsx'
import { BrowserRouter, Route, Routes } from 'react-router-dom'

createRoot(document.getElementById('root')!).render(
//  <StrictMode>
  //  <App />
 // </StrictMode>,

<StrictMode>
  <BrowserRouter>
    <Routes>  
      <Route path = "/" element={<App />} />
      <Route path = "/About" element={<About />} />
    </Routes>
  </BrowserRouter>
</StrictMode>


,
)
