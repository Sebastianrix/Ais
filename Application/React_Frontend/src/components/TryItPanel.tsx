import { Play, Loader2 } from 'lucide-react';
import type { ApiResponse } from './hooks/useApiFrame';

type Props = {
  response: ApiResponse;
  loading: boolean;
};

function StatusBadge({ status }: { status: number }) {
  const tone =
    status >= 200 && status < 300 ? 'ok' :
    status >= 400 && status < 500 ? 'warn' :
    status >= 500 ? 'err' :
    'unknown';
  return <span className={`status-badge status-${tone}`}>{status || 'ERR'}</span>;
}

export default function TryItPanel({ response, loading }: Props) {
  return (
    <aside className="try-panel">
      <div className="try-panel-header">
        <span className="try-panel-title">Response</span>
        {response && !loading && (
          <span className="try-panel-meta">
            <StatusBadge status={response.status} />
            <span className="duration">{response.duration}ms</span>
          </span>
        )}
      </div>

      <div className="try-panel-body">
        {loading && (
          <div className="try-panel-loading">
            <Loader2 className="spinner" size={20} />
            <span>Sending request...</span>
          </div>
        )}

        {!loading && !response && (
          <div className="try-panel-empty">
            <Play size={32} strokeWidth={1.5} />
            <p>Click <strong>Try it</strong> on any endpoint to see a live response.</p>
          </div>
        )}

        {!loading && response && (
          <>
            <div className="try-panel-request">
              <span className={`method method-${response.method.toLowerCase()}`}>
                {response.method}
              </span>
              <span className="try-panel-url">{response.url}</span>
            </div>
            <pre className="try-panel-response">{response.body}</pre>
          </>
        )}
      </div>
    </aside>
  );
}
