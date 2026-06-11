//import getTankers from 'ApiService.tsx';
import getTankers from "@/components/ApiService";
import styles from '../css/EntryBrowserMenu.module.css';
function Menu() {
  return (
    <div className={styles.box}>
    <p><p>
The system processes AIS tanker data and presents it through an interactive map and web interface.
</p>
</p>
    </div>
  );
}
export default Menu;