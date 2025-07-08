import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

type Airport = [string, string];

// Inside the component
const navigate = useNavigate();
const [loading, setLoading] = useState(false);

export function FlightSearchForm() {
  const [airports, setAirports] = useState<Airport[]>([]);
  const [formData, setFormData] = useState({
    airportFrom: "",
    airportTo: "",
    departureRangeFrom: "",
    departureRangeTo: "",
    countAdults: 1,
  });

  useEffect(() => {
    fetch("/getAirports")
      .then((res) => res.json())
      .then((data) => setAirports(data.airports));
  }, []);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === "countAdults" ? parseInt(value) : value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true); // Show loading indicator

    const params = new URLSearchParams(formData as any).toString();
    const res = await fetch(`/searchFlights?${params}`);
    const data = await res.json();

    setLoading(false); // Hide loading indicator
    navigate("/results", { state: { results: data.queries, summary: data.flights_search } });
  };

  return (
    <div className="max-w-xl mx-auto p-6 bg-white dark:bg-gray-900 rounded shadow">
      <h1 className="text-2xl font-bold mb-6 text-gray-800 dark:text-gray-100">Flight Search</h1>
      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label htmlFor="airportFrom" className="block text-sm font-medium text-gray-700 dark:text-gray-300">
            From
          </label>
          <select
            id="airportFrom"
            name="airportFrom"
            onChange={handleChange}
            required
            className="mt-1 block w-full rounded border-gray-300 dark:border-gray-700 dark:bg-gray-800 dark:text-white shadow-sm focus:ring-blue-500 focus:border-blue-500"
          >
            <option value="">Select airport</option>
            {airports.map(([name, code]) => (
              <option key={code} value={code}>
                {name.replace("_", " ").replace(/\b\w/g, (c) => c.toUpperCase())} ({code})
              </option>
            ))}
          </select>
        </div>

        <div>
          <label htmlFor="airportTo" className="block text-sm font-medium text-gray-700 dark:text-gray-300">
            To
          </label>
          <select
            id="airportTo"
            name="airportTo"
            onChange={handleChange}
            required
            className="mt-1 block w-full rounded border-gray-300 dark:border-gray-700 dark:bg-gray-800 dark:text-white shadow-sm focus:ring-blue-500 focus:border-blue-500"
          >
            <option value="">Select airport</option>
            {airports.map(([name, code]) => (
              <option key={code} value={code}>
                {name.replace("_", " ").replace(/\b\w/g, (c) => c.toUpperCase())} ({code})
              </option>
            ))}
          </select>
        </div>

        <div>
          <label htmlFor="departureRangeFrom" className="block text-sm font-medium text-gray-700 dark:text-gray-300">
            Min departure date
          </label>
          <input
            type="date"
            name="departureRangeFrom"
            title="departureRangeFrom"
            required
            onChange={handleChange}
            className="mt-1 block w-full rounded border-gray-300 dark:border-gray-700 dark:bg-gray-800 dark:text-white shadow-sm focus:ring-blue-500 focus:border-blue-500"
          />
        </div>

        <div>
          <label htmlFor="departureRangeTo" className="block text-sm font-medium text-gray-700 dark:text-gray-300">
            Max departure date
          </label>
          <input
            type="date"
            name="departureRangeTo"
            title="departureRangeTo"
            required
            onChange={handleChange}
            className="mt-1 block w-full rounded border-gray-300 dark:border-gray-700 dark:bg-gray-800 dark:text-white shadow-sm focus:ring-blue-500 focus:border-blue-500"
          />
        </div>

        <div>
          <label htmlFor="countAdults" className="block text-sm font-medium text-gray-700 dark:text-gray-300">
            Adults
          </label>
          <input
            type="number"
            name="countAdults"
            title="countAdults"
            min={1}
            required
            value={formData.countAdults}
            onChange={handleChange}
            className="mt-1 block w-full rounded border-gray-300 dark:border-gray-700 dark:bg-gray-800 dark:text-white shadow-sm focus:ring-blue-500 focus:border-blue-500"
          />
        </div>

        <div>
          <button
            type="submit"
            className="w-full bg-blue-600 hover:bg-blue-700 text-white font-semibold py-2 px-4 rounded shadow"
          >
            Start search
          </button>
        </div>
      </form>
      {loading && <p className="text-blue-500">Searching for flights...</p>}
    </div>
  );
}
