import { } from 'react'
import './Navbar.css'

const Navbar = () => {
  return (

<nav className="navbar">
  <div className= "navbar-left">
    <a href="/" className="logo">   
    AisMapDanmarkLogo
    </a>
  </div>

<div className="navbar-center">
  <ul className="nav-links">
    <li>
      <a href="/map">Map</a>
    </li>
    <li>
      <a href="/landing">Landing Page</a>
    </li>
    <li>
      <a href="/about">About Us</a>
    </li>
    <li>
      <a href="/faq">FAQ</a>
    </li>
  </ul>
</div>
  




</nav>
  )
};

export default Navbar;

