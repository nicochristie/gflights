from datetime import date, timedelta
from enum import Enum
from fast_flights import Airport, create_filter, get_flights_from_filter, FlightData, Passengers, search_airport

class GetModes(Enum):
    COMMON = "common"
    FALLBACK = "fallback"
    FORCE_FALLBACK = "force-fallback"
    LOCAL = "local"

def getFlight(departFrom, arriveAt, departOn, returnOn = None):
    filter = create_filter(
        flight_data=[
            # Include more if it's not a one-way trip
            FlightData(
                date=departOn,  # Date of departure
                from_airport=departFrom,  # Departure (airport)
                to_airport=arriveAt,  # Arrival (airport)
            )
        ],
        trip="one-way",  # Trip type
        passengers=Passengers(adults=2, children=1, infants_in_seat=1, infants_on_lap=0),  # Passengers
        seat="economy",  # Seat type
        max_stops=1,  # Maximum number of stops
    )

    print(filter.as_b64().decode("utf-8"))
    print(get_flights_from_filter(filter, mode=GetModes.COMMON.value))

# search_airport('Charles de Gaulle')[0].value
departFrom = Airport.LOS_ANGELES_INTERNATIONAL_AIRPORT.value 
arriveAt = Airport.SAN_DIEGO_INTERNATIONAL_AIRPORT.value
departOn = (date.today() + timedelta(weeks=4)).strftime('%Y-%m-%d')
print(f'From {departFrom} to {arriveAt} on {departOn}')

getFlight(departFrom, arriveAt, departOn)
