import { useState, useEffect} from 'react'
import { Search } from "lucide-react";



import styles from '../css/Menu.module.css';

import { statsService } from "../services/StatsService";
import type { Stats } from "../types/Stats";




function Menu() {



  const [stats, setStats] = useState<Stats | null>(null);
  const [loading, setLoading] = useState(true);
  const [query, setQuery] = useState({
  page: 1,
  pageSize: 50,
  search: "",
});


const load = async () => {
  const data = await tankerService.getAll(query);
  setTankers(data);
};




useEffect(() => {
  load();
}, [query]);


  useEffect(() => {
    const loadStats = async () => {
      try {
        const data = await statsService.get();
        setStats(data);
       
      } catch (err) {
        console.error("Failed to load stats", err);
      } finally {
        setLoading(false);
      }
    };

    loadStats();
  }, []);

  






  
return (
  <div className={styles.box}>
    <div className={styles.searchBox}>
      <Search className={styles.icon} size={18} />
      <input
        value={query.search}
        onChange={(e) =>
          setQuery((prev) => ({
            ...prev,
            search: e.target.value,
            page: 1,
          }))
        }
        placeholder="Search..."
      />
    </div>

    <button
      onClick={() =>
        setQuery((p) => ({ ...p, page: p.page - 1 }))
      }
      disabled={query.page === 1}
    >
      Prev
    </button>

    <span>Page {query.page}</span>

    <button
      onClick={() =>
        setQuery((p) => ({ ...p, page: p.page + 1 }))
      }
    >
      Next
    </button>
  </div>
);
}
export default Menu;