import { useState, useEffect } from 'react';
import '../css/SidePanel.css';
import { Book, Key, Zap, AlertCircle, Code } from 'lucide-react';
import EndpointCard from './EndpointCard';
import TryItPanel from './TryItPanel';
import { useApiFrame } from './hooks/useApiFrame';

const sections = [
  {
    id: 'getting-started',
    title: 'Getting Started',
    Icon: Book,
    subsections: [
      { id: 'overview', title: 'Overview' },
      { id: 'base-url', title: 'Base URL' },
      { id: 'quick-test', title: 'Quick Test' },
    ],
  },
  {
    id: 'endpoints',
    title: 'Endpoints',
    Icon: Code,
    subsections: [
      { id: 'stats', title: 'Stats' },
      { id: 'tanker-positions', title: 'Tanker Positions' },
      { id: 'tankers', title: 'Tankers' },
      { id: 'tanker-staging', title: 'Tanker Staging' },
      { id: 'tracked-tanker', title: 'Tracked Tanker' },
      { id: 'swagger', title: 'Swagger UI' },
    ],
  },
  {
    id: 'authentication',
    title: 'Authentication',
    Icon: Key,
    subsections: [
      { id: 'auth-overview', title: 'Overview' },
      { id: 'rate-limits', title: 'Rate Limits' },
    ],
  },
  {
    id: 'responses',
    title: 'Responses',
    Icon: Zap,
    subsections: [
      { id: 'response-format', title: 'Format' },
      { id: 'example-response', title: 'Example' },
    ],
  },
  {
    id: 'errors',
    title: 'Errors',
    Icon: AlertCircle,
    subsections: [
      { id: 'status-codes', title: 'Status Codes' },
    ],
  },
];

export default function SidePanel() {
  const [collapsed, setCollapsed] = useState(false);
  const [activeId, setActiveId] = useState('overview');
  const [expandedSections, setExpandedSections] = useState<Set<string>>(
    new Set(['getting-started'])
  );

  const { response, loading, send } = useApiFrame();

  useEffect(() => {
    const allSubsectionIds = sections.flatMap(s =>
      s.subsections.map(sub => sub.id)
    );

    const observer = new IntersectionObserver(
      (entries) => {
        const visible = entries
          .filter(entry => entry.isIntersecting)
          .sort((a, b) => a.boundingClientRect.top - b.boundingClientRect.top);
        if (visible.length > 0) {
          setActiveId(visible[0].target.id);
        }
      },
      { rootMargin: '-20% 0px -70% 0px', threshold: 0 }
    );

    allSubsectionIds.forEach(id => {
      const el = document.getElementById(id);
      if (el) observer.observe(el);
    });

    return () => observer.disconnect();
  }, []);

  useEffect(() => {
    const parent = sections.find(s =>
      s.subsections.some(sub => sub.id === activeId)
    );
    if (parent) {
      setExpandedSections(prev => new Set(prev).add(parent.id));
    }
  }, [activeId]);

  const toggleSection = (sectionId: string) => {
    setExpandedSections(prev => {
      const next = new Set(prev);
      if (next.has(sectionId)) next.delete(sectionId);
      else next.add(sectionId);
      return next;
    });
  };

  const scrollToSection = (id: string) => {
    const el = document.getElementById(id);
    if (el) el.scrollIntoView({ behavior: 'smooth', block: 'start' });
  };

  return (
  <div className={`layout has-try-panel ${collapsed ? 'collapsed' : ''}`}>

      <aside className="side-panel">
        <button
          className="toggle-btn"
          onClick={() => setCollapsed(!collapsed)}
          aria-label={collapsed ? 'Expand panel' : 'Collapse panel'}
        >
          {collapsed ? '›' : '‹'}
        </button>

        <nav className="nav-list">
          {sections.map(section => {
            const isExpanded = expandedSections.has(section.id);
            const sectionHasActive = section.subsections.some(
              sub => sub.id === activeId
            );
            const SectionIcon = section.Icon;

            return (
              <div key={section.id} className="nav-section">
                <button
                  className={`section-header ${sectionHasActive ? 'has-active' : ''}`}
                  onClick={() => {
                    if (collapsed) {
                      scrollToSection(section.subsections[0].id);
                    } else {
                      toggleSection(section.id);
                    }
                  }}
                  title={collapsed ? section.title : undefined}
                >
                  <span className="section-icon">
                    <SectionIcon size={18} />
                  </span>
                  {!collapsed && (
                    <>
                      <span className="section-title">{section.title}</span>
                      <span className={`chevron ${isExpanded ? 'open' : ''}`}>▸</span>
                    </>
                  )}
                </button>

                {!collapsed && (
                  <ul className={`subsections ${isExpanded ? 'expanded' : ''}`}>
                    {section.subsections.map(sub => (
                      <li key={sub.id}>
                        <button
                          className={`sub-item ${activeId === sub.id ? 'active' : ''}`}
                          onClick={() => scrollToSection(sub.id)}
                        >
                          {sub.title}
                        </button>
                      </li>
                    ))}
                  </ul>
                )}
              </div>
            );
          })}
        </nav>
      </aside>

      <main className="content api-content">
        <header className="api-header">
          <span className="api-badge">v1</span>
          <h1>AIS Map Public API</h1>
          <p className="api-subtitle">
            Free, open access to Baltic Sea tanker tracking data
          </p>
        </header>

        <section id="getting-started">
          <section id="overview" className="subsection">
            <h2 className="section-label">Overview</h2>
            <div className="prose">
              <p>
                Welcome to the AIS Map API. This is where you can find information
                about our endpoints for accessing Baltic Sea tanker tracking data.
              </p>
              <p>
                We provide this platform free of charge. If you are a scientist,
                researcher, or developer, our API is freely available for your use.
              </p>
              <div className="callout callout-warning">
                <strong>Tanker data only.</strong> We currently only return data for
                tanker vessels. The API does <em>not</em> return data on cargo ships,
                fishing vessels, passenger ships, or other vessel types.
              </div>
            </div>
          </section>

          <section id="base-url" className="subsection">
            <h2 className="section-label">Base URL</h2>
            <div className="code-block">
              <code>https://api.aismap.dk</code>
            </div>
            <p className="prose">
              All endpoints are served over HTTPS and return <code>application/json</code>.
            </p>
          </section>

          <section id="quick-test" className="subsection">
            <h2 className="section-label">Quick Test</h2>
            <p className="prose">
              Open this URL in your browser to verify connectivity:
            </p>
            <EndpointCard href="https://api.aismap.dk/stats" path="/stats" onTry={send} />
            <p className="prose grey-text">
              If you see a JSON response, you're good to go.
            </p>
          </section>
        </section>

        <section id="endpoints">
          <section id="stats" className="subsection">
            <h3 className="section-label">Stats</h3>
            <p className="prose">Returns aggregate statistics about the dataset.</p>
            <EndpointCard href="https://api.aismap.dk/stats" path="/stats" onTry={send} />
          </section>

          <section id="tanker-positions" className="subsection">
            <h3 className="section-label">Tanker Positions</h3>
            <p className="prose">
              Returns the latest AIS positions for tracked tanker vessels.
            </p>
            <EndpointCard
              href="https://api.aismap.dk/TankerPositions"
              path="/TankerPositions"
              onTry={send}
            />
          </section>

          <section id="tankers" className="subsection">
            <h3 className="section-label">Tankers</h3>
            <p className="prose">Returns the registry of known tanker vessels.</p>
            <EndpointCard href="https://api.aismap.dk/tanker" path="/Tankers" onTry={send} />
          </section>

          <section id="tanker-staging" className="subsection">
            <h3 className="section-label">Tanker Staging</h3>
            <p className="prose">
              Returns staged tanker position data prior to enrichment.
            </p>
            <EndpointCard
              href="https://api.aismap.dk/tankerstaging"
              path="/TankerStaging"
              onTry={send}
            />
          </section>

          <section id="tracked-tanker" className="subsection">
            <h3 className="section-label">Tracked Tanker</h3>
            <p className="prose">
              Returns vessels currently flagged as being on the shadow fleet watchlist.
            </p>
            <EndpointCard
              href="https://api.aismap.dk/trackedtanker"
              path="/TrackedTanker"
              onTry={send}
            />
          </section>

          <section id="swagger" className="subsection">
            <h3 className="section-label">Swagger UI</h3>
            <p className="prose">
              Interactive API explorer with request/response schemas.
            </p>
            <EndpointCard href="https://api.aismap.dk/swagger" path="/swagger" onTry={send} />
            <p className="prose grey-text">More endpoints coming soon.</p>
          </section>
        </section>

        <section id="authentication">
          <section id="auth-overview" className="subsection">
            <h2 className="section-label">Authentication</h2>
            <p className="prose">
              The AIS Map API is currently public and requires no authentication.
              All endpoints accept anonymous requests.
            </p>
            <div className="callout">
              <strong>Coming soon:</strong> API keys for higher rate limits and
              access to historical position queries.
            </div>
          </section>

          <section id="rate-limits" className="subsection">
            <h2 className="section-label">Rate Limits</h2>
            <p className="prose">
              Unauthenticated requests are limited to a fair-use threshold. Excessive
              polling may be temporarily blocked. If you have a research use case
              that requires bulk access, please get in touch.
            </p>
          </section>
        </section>

        <section id="responses">
          <section id="response-format" className="subsection">
            <h2 className="section-label">Response Format</h2>
            <p className="prose">
              All responses are returned as JSON with{' '}
              <code>Content-Type: application/json</code>. Timestamps are ISO 8601
              in UTC. Coordinates are decimal degrees (WGS 84).
            </p>
          </section>

          <section id="example-response" className="subsection">
            <h2 className="section-label">Example Response</h2>
            <p className="prose">
              A trimmed response from <code>/api/v1/TankerPositions</code>:
            </p>
            <pre className="code-block code-block-multiline">
{`[
  {
    "mmsi": 209123000,
    "imo": 9456789,
    "name": "EXAMPLE TANKER",
    "lat": 55.6761,
    "lon": 12.5683,
    "sog": 12.4,
    "cog": 87.3,
    "timestamp": "2026-05-12T14:23:11Z",
    "flag": "CY"
  }
]`}
            </pre>
          </section>
        </section>

        <section id="errors">
          <section id="status-codes" className="subsection">
            <h2 className="section-label">Status Codes</h2>
            <table className="status-table">
              <thead>
                <tr>
                  <th>Code</th>
                  <th>Meaning</th>
                </tr>
              </thead>
              <tbody>
                <tr><td><code>200</code></td><td>Success</td></tr>
                <tr><td><code>404</code></td><td>Resource not found</td></tr>
                <tr><td><code>429</code></td><td>Rate limit exceeded</td></tr>
                <tr><td><code>500</code></td><td>Server error — try again later</td></tr>
                <tr><td><code>503</code></td><td>Service temporarily unavailable</td></tr>
              </tbody>
            </table>
          </section>
        </section>
      </main>

      <TryItPanel response={response} loading={loading} />
    </div>
  );
}
