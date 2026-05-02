//import getTankers from 'ApiService.tsx';
import getTankers from "@/components/ApiService";
function Menu() {
  return (
    <div className="bg-stone-900 text-white p-7 rounded-2xl max-w-xs">
      <p className="text-xs uppercase tracking-wide text-amber-200">
        Lemo lupis latin win win nice
      </p>
      <p className="text-5xl font-bold mt-3">
        Did we win yet?<span className="text-base font-normal">/mo</span>
      </p>
      <button className="mt-6 w-full bg-amber-200 text-stone-900 py-2.5 rounded-md hover:bg-amber-100 transition">
       Victory
      </button>
    </div>
  );
}
export default Menu;