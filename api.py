from datetime import datetime
from flask import Flask, jsonify, request
from flask_cors import CORS
from fast_flights import Airport, Passengers
from helpers.helpers import getQueries

app = Flask(__name__)
CORS(app)

@app.route("/getAirports")
def getAirports():
    airports = [(airport.name, airport.value) for airport in Airport]
    return jsonify({ "airports": airports })

@app.route("/searchFlights")
def searchflights():
    departFrom = request.args.get("airportFrom")
    arriveAt = request.args.get("airportTo")
    minDepartureDate = datetime.strptime(request.args.get("departureRangeFrom"), '%Y-%m-%d').date()
    maxDepartureDate = datetime.strptime(request.args.get("departureRangeTo"), '%Y-%m-%d').date()
    countAdults = int(request.args.get("countAdults"))

    passengers = Passengers(adults=countAdults, children=0, infants_in_seat=0, infants_on_lap=0)
    queries = getQueries(departFrom, arriveAt, minDepartureDate, maxDepartureDate, passengers)    
   
    return jsonify({ "queries": queries })

if __name__ == "__main__":
    app.run(debug=True)
