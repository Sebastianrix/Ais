import { useState, useEffect} from 'react'
import { Search } from "lucide-react";


import styles from '../css/Menu.module.css';

import { tankerService } from "../services/TankerService";
import type { Tanker } from "../types/Tankers";



function Menu() {
  
  const [tankers, setTankers] = useState<Tanker[]>([]);

  
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

    <button className={styles.pagePaddles}
      onClick={() =>
        setQuery((p) => ({ ...p, page: p.page - 1 }))
      }
      disabled={query.page === 1}
    >
      Prev
    </button>

    <span className={styles.pageIndexNumber}> <strong>{query.page}</strong></span>

    <button className={styles.pagePaddles}
      onClick={() =>
        setQuery((p) => ({ ...p, page: p.page + 1 }))
      }
    >
      Next
    </button>

<div>
  {tankers.map((tanker) => (
<div className={styles.resultRow}>
  <span>{tanker.vessel_Name}</span>
  <span>{tanker.imo}</span>
  <span>{tanker.flag}</span>
</div>
  ))}
</div>

  </div>
);
}
export default Menu;