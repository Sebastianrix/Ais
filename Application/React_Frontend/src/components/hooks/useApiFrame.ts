import { useState } from 'react';

export type ApiResponse = {
  status: number;
  duration: number;
  body: string;
  method: string;
  url: string;
} | null;

export function useApiFrame() {
  const [response, setResponse] = useState<ApiResponse>(null);
  const [loading, setLoading] = useState(false);

  const send = async (method: string, url: string) => {
    setLoading(true);
    const start = performance.now();
    try {
      const res = await fetch(url, { method });
      const text = await res.text();
      let body = text;
      try {
        body = JSON.stringify(JSON.parse(text), null, 2);
      } catch {
        // Not JSON, keep as raw text
      }

      setResponse({
        status: res.status,
        duration: Math.round(performance.now() - start),
        body: body.slice(0, 5000),
        method,
        url,
      });
    } catch (err) {
      const message = err instanceof Error ? err.message : 'unknown error';
      setResponse({
        status: 0,
        duration: Math.round(performance.now() - start),
        body: `Network error: ${message}\n\nThis usually means CORS isn't configured on the API, or the API is unreachable.`,
        method,
        url,
      });
    } finally {
      setLoading(false);
    }
  };

  return { response, loading, send };
}
