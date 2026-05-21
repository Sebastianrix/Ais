import SidePanel from '../components/SidePanel';
 //  import {TryItPanel] from '../components/TryItPanel';
import '../css/APIpage.css';
import Navbar from '@/components/Navbar';

function APIpage() {
  return (
    <>
      <Navbar />
 {//   <TryItPanel/>
 }
      <SidePanel />
    </>
  );
}

export default APIpage;