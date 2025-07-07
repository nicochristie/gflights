from enum import Enum
from fast_flights import create_filter, get_flights_from_filter, FlightData, Passengers
from datetime import date, datetime, timedelta

class DateFormat(Enum):
    Googleish = "%I:%M %p on %a, %b %d %Y"
    Standard = "%d-%m-%Y %H:%M"

class GetModes(Enum):
    COMMON = "common"
    FALLBACK = "fallback"
    FORCE_FALLBACK = "force-fallback"
    LOCAL = "local"

def getFlight(departFrom, arriveAt, departOn, returnOn = None, passengers: Passengers = Passengers(adults=1, children=0, infants_in_seat=0, infants_on_lap=0)):
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
        passengers=passengers,
        seat="economy",  # Seat type
        max_stops=1,  # Maximum number of stops
    )

    return get_flights_from_filter(filter, mode=GetModes.COMMON.value)
    #print(filter.as_b64().decode("utf-8"))

def dateRange(start_date: date, end_date: date, endIncluded: bool = True):
    days = int((end_date - start_date).days) + 1 if endIncluded else 0
    for n in range(days):
        yield start_date + timedelta(n)

def getQueries(departFrom: str, arriveAt: str, minDepartureDate: date, maxDepartureDate: date, passengers: Passengers = None):
    queries = []
    for departOn in dateRange(minDepartureDate, maxDepartureDate):
        print(f'Query for {departOn}')
        flight_plan = f'On {departOn}'
        result = getFlight(departFrom, arriveAt, departOn.strftime('%Y-%m-%d'), None, passengers)
        formatted_flights = []
        for flight in result.flights:
            d = datetime.strptime(f'{flight.departure} {departOn.year}', DateFormat.Googleish.value)
            a = datetime.strptime(f'{flight.arrival} {departOn.year}', DateFormat.Googleish.value)
            formatted_flights.append({
                "is_best": flight.is_best,
                "name": flight.name,
                "departure": datetime.strftime(d, DateFormat.Standard.value),
                "arrival": datetime.strftime(a, DateFormat.Standard.value),
                "arrival_time_ahead": flight.arrival_time_ahead,
                "duration": flight.duration,
                "stops": flight.stops,
                "price": flight.price
            })
        
        queries.append({ "label": flight_plan, "flights": formatted_flights })

    return queries
