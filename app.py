from datetime import datetime
from enum import Enum
from flask import Flask, render_template, request
from fast_flights import Airport, Passengers
from helpers.helpers import getQueries

class Pages(Enum):
    Index = "index.html"
    Tabs = "tabs.html"

app = Flask(__name__)

@app.route("/")
def index():
    airports = [(airport.name, airport.value) for airport in Airport]
    return render_template(Pages.Index.value, airports=airports)

@app.route("/search")
def show_flights():
    departFrom = request.args.get("airportFrom")
    arriveAt = request.args.get("airportTo")
    minDepartureDate = datetime.strptime(request.args.get("departureRangeFrom"), '%Y-%m-%d').date()
    maxDepartureDate = datetime.strptime(request.args.get("departureRangeTo"), '%Y-%m-%d').date()
    countAdults = int(request.args.get("countAdults"))

    flights_search = f'From {departFrom} to {arriveAt} in range {minDepartureDate} to {maxDepartureDate}'
    passengers = Passengers(adults=countAdults, children=0, infants_in_seat=0, infants_on_lap=0)
    queries = getQueries(departFrom, arriveAt, minDepartureDate, maxDepartureDate, passengers)    
   
    return render_template(Pages.Tabs.value, flights_search=flights_search, queries=queries)

if __name__ == "__main__":
    app.run(debug=True)
