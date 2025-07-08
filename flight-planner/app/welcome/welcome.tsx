type WelcomeProps = {
  airports: [string, string][];
};

export function Welcome({ airports }: WelcomeProps) {
  return (
    <div>
      <h1>Welcome to Flight Planner</h1>
      <label htmlFor="airport-select">Choose an airport:</label>
      <select id="airport-select">
        {airports.map(([name, value]) => (
          <option key={value} value={value}>
            {name}
          </option>
        ))}
      </select>
    </div>
  );
}
