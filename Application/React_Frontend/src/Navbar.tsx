import { } from 'react'
import './Navbar.css'

const Navbar = () => {
  return (

<nav className="navbar" role="navigation">
  <div className= "navbar-left">
    <a href="/" className="logo">   
    Logo
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


<div className="navbar-right">
  <a href="/search" className="search-logo">
    <i className="fas fa-search"></i>
    <span className="count">0</span>
  </a>
  <a href="/account" className="user-icon">
    <i className="fas fa-user"></i>
  </a>
</div>




</nav>
  )
};

export default Navbar;

