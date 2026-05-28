import { useState } from 'react';
import Navbar from '../components/Navbar';
import Footbar from '../components/Footbar';
import styles from '../css/FAQ.module.css';

export default function FAQ() {
  // Simple state array to handle accordion toggles independently
  const [openIndex, setOpenIndex] = useState<number | null>(null);

  const toggleAccordion = (index: number) => {
    setOpenIndex(openIndex === index ? null : index);
  };

  const faqData = [
    {
      category: "General Project Information",
      questions: [
        {
          q: "What is the purpose of aismap.dk?",
          a: "This platform was developed as part of a Master's thesis in Computer Science at Roskilde University. It provides an automated, scalable framework to ingest, clean, and visualize large-scale historical and new AIS (Automatic Identification System) data, focusing explicitly on anomaly analysis for tanker vessels."
        },
        {
          q: "Where does this data come from?",
          a: "Our data is sourced directly from the openly available historical archives of the Danish Maritime Authority (https://aisdata.ais.dk). It captures marine traffic telemetry recorded within Danish territorial waters."
        },
        {
          q: "Is this a real-time vessel tracking network?",
          a: "No. The platform operates with a fixed 72-hour delay dictated directly by the upstream data provider's release schedule. Our pipeline is specifically engineered to align with this 3-day synchronization window, ensuring that we ingest fully finalized daily archives from the maritime authorities rather than dealing with incomplete or missing data fragments. This optimizes the platform for stable, deep historical analytics rather than live tracking."
        }
      ]
    },
    {
      category: "Hosting & Infrastructure",
      questions: [
        {
          q: "Where is this platform hosted?",
          a: "To ensure complete data sovereignty and avoid external platform dependencies, aismap.dk is hosted entirely within Denmark on our own dedicated, bare-metal hardware. The entire storage, processing, and delivery pipeline runs on an on-premise Ubuntu server stack operating under physical constraints we fully control."
        },
        {
          q: "How large is the underlying dataset?",
          a: "The platform processes an extensive archive spanning over two billion raw position reports (over 2.04 billion staging rows) capturing traffic telemetry from 2026 alone. Furthermore, a historical data backfill operation is actively ongoing, extending our coverage back to 2007."
        },
        {
          q: "What technologies power the stack?",
          a: "The infrastructure is containerized via Docker. It utilizes Apache Airflow for workflow orchestration, a PostgreSQL data warehouse layer, an ASP.NET Core (v8.0) web service layer, and an interactive React/TypeScript client application with MapLibre GL."
        }
      ]
    },
    {
      category: "Anomaly Tracking & Heuristics",
      questions: [
        {
          q: "What qualifies as an identity or flag irregularity?",
          a: "The system maps the system-reported Maritime Mobile Service Identity (MMSI) against international ITU assignments. We isolate anomalies into distinct categories, such as an Invalid MID (unresolved country prefix), an Unknown IMO (absent or malformed identity numbers), or an MMSI Swap (a single hull identity broadcasting across multiple distinct MMSI profiles over time)."
        },
        {
          q: "Does a flagged vessel mean it is definitely participating in illicit behavior?",
          a: "No. The system flags candidate signals, not legal adjudications. In an open, self-reported broadcasting standard like AIS, inconsistencies can occur due to data drift, outdated static lookup tables, sensory bugs, or lawful ownership re-registration following a commercial sale. The platform provides data transparency so researchers can further audit these entries honestly."
        }
      ]
    }
  ];

  //  Flat array index for only 1 FAQ open at a time
  let globalIndex = 0;

  return (
    <>  
      <Navbar />

      <div className={styles.faqContainer}>
        <div className={styles.faqHeader}>
          <h1>Frequently Asked Questions</h1>
          <p>Find technical answers regarding our platform, data pipeline, and infrastructure architecture.</p>
        </div>

        <div className={styles.faqContent}>
          {faqData.map((section, sectionIdx) => (
            <div key={sectionIdx} className={styles.faqSection}>
              <h2 className={styles.sectionTitle}>{section.category}</h2>
              
              {section.questions.map((item) => {
                const currentIndex = globalIndex++;
                const isOpen = openIndex === currentIndex;

                return (
                  <div key={currentIndex} className={`${styles.accordionItem} ${isOpen ? styles.active : ''}`}>
                    <button 
                      className={styles.accordionHeader} 
                      onClick={() => toggleAccordion(currentIndex)}
                      aria-expanded={isOpen}
                    >
                      <span>{item.q}</span>
                      <span className={styles.icon}>{isOpen ? '−' : '+'}</span>
                    </button>
                    
                    <div className={styles.accordionBody}>
                      <p>{item.a}</p>
                    </div>
                  </div>
                );
              })}
            </div>
          ))}
        </div>
      </div>

    </>
  );
}
