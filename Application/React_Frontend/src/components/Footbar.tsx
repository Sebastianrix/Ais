import { } from 'react'
import '../css/Footbar.css'

const Footbar = () => {
    return (
<div className="main-footer">
    <div className="container">
        <div className="row">
            <div className="col">
                <h4>AIS MAP TEAM</h4>
                <ul className="list-unstyled">
                    <li>Roskilde Universitet<</li>
                      <li>Mester Thesis team of two.</li>  
                              <li>Hosted within the EU, in Denmark</li>  
                </ul>
            </div>
                <div className="col">
                <h4>Contacts</h4>
                <ul className="list-unstyled">
                    <li>+45 22 12 88 42</li>
                    <li>srix@ruc.dk</li>
                    <li>stud-neha@ruc.dk</li>
                    <li>Universitetsvej 1, 4000</li>
                    <li>Roskilde, Danmark</li>

                           
                </ul>
            </div>
            <div className="col text-center" > 
                <h4 className="text-center">Sites</h4> 
                <ul className="footbar-links"> 
                    <li> <a href="/map">Map</a> 
                    </li> 
                    <li> 
                        <a href="/APIpage">API</a> 
                    </li> 
                    <li> 
                        <a href="/about">About Us</a> 
                    </li> 
                    <li>
                    <a href="/faq">FAQ</a> 
                    </li>     
                </ul>
            </div>
        </div>
        <hr />
            <div className="row">
                <p className="col-sm">
                    &copy;{new Date().getFullYear()} Master thesis group at Roskilde University | All rights reserved | Terms of Service | Privacy
                </p>
                
            </div>
    </div>
</div>

    )
};

export default Footbar;
