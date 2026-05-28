import '../css/SidePanel.css'

type EndpointCardProps = {
  href: string;
  method?: string;
  path: string;
  onTry?: (method: string, url: string) => void;
};

export default function EndpointCard({
  href,
  method = 'GET',
  path,
  onTry,
}: EndpointCardProps) {
  return (
    <div className="endpoint-row">
      <div className="endpoint-card">
        <span className={`method method-${method.toLowerCase()}`}>{method}</span>
        <span className="endpoint-path">{path}</span>
      </div>
      
      {/* Only render the Try button if an onTry handler is passed down */}
      {onTry && (
        <button
          className="try-button"
          onClick={() => onTry(method, href)}
        >
          Try it →
        </button>
      )}
      
      <a
        className="external-link"
        href={href}
        target="_blank"
        rel="noreferrer"
        title="Open in new tab"
      >
        ↗
      </a>
    </div>
  );
}