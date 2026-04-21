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
                    <li>+45 22 12 88 42</li>
                    <li>Roskilde, Danmark</li>
                    <li>Lysalleen 180</li>               
                </ul>
            </div>
                <div className="col">
                <h4>Contacts</h4>
                <ul className="list-unstyled">
                    <li>Sebastian Rix</li>
                    <li>Sebastianrix11@gmail.com</li>
                    <li>Roskilde Universitet</li>               
                </ul>
            </div>
                <div className="col">
                <h4>Sites</h4>
                <ul className="list-unstyled">
                    <li>Map</li>
                    <li>API</li>  
                    <li>About</li>
                    <li>FQA</li>               
                </ul>
            </div>
        </div>
    </div>
</div>

    )
};

export default Footbar;