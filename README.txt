USAGE:

- Find lat/long from address:
  string address = "1600 Amphitheatre Parkway, Mountain View, CA 94043";
  Coordinate coordinate = new LocationService().GetLatLongFromAddress(address);

- Find address from lat/long:
  Coordinate origin = new Coordinate { Latitude = 37.4217429, Longitude = -122.0844308 };
  string address = new LocationService().GetAddressFromLatLong(origin.Latitude, origin.Longitude)  

- Get directions between two addresses
  string origin = "1600 Amphitheatre Parkway, Mountain View, CA 94043";
  string destination = "1701 Airport Blvd  San Jose, CA 95110";
  Directions directions = new LocationService().GetDirections(origin, destination, TravelMode.Walking);

  string totalDistance = directions.Distance;
  string totalDuration = directions.Duration;

  foreach (DirectionStep step in directions.Steps)
  {
    // step.Instruction
    // step.Duration
  }