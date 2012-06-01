using Geocoder.Enums;

namespace Geocoder.Interfaces
{
    public interface IGeocoder
    {
        /// <summary>
        /// Attemps to retrieve the address associated with the given coordinates.
        /// </summary>
        /// <param name="latitude">The latitude portion of the coordinate.</param>
        /// <param name="longitude">The longitude portion of the coordinate.</param>
        /// <returns>An <see cref="Address" /> object, or null if the address could not be located.</returns>
        Address GetAddressFromLatLong(double latitude, double longitude);

        /// <summary>
        /// Attemps to retrieve the coordinate associated with the given address.
        /// </summary>
        /// <param name="address">The address to locate.</param>
        /// <returns>A <see cref="Coordinate" /> object, or null if the address could not be located.</returns>
        Coordinate GetLatLongFromAddress(string address);

        /// <summary>
        /// Gets a <see cref="Directions"/> object for the driving path between two coordinates.
        /// </summary>
        /// <param name="origin">A <see cref="Coordinate"/> object representing the origin location.</param>
        /// <param name="destination">A <see cref="Coordinate"/> object representing the destination location.</param>
        /// <param name="travelMode">A <see cref="TravelMode"/> enum value corresponding to the desired mode of travel.</param>
        /// <returns>A <see cref="Directions"/> object, or null if no path exists.</returns>
        Directions GetDirections(Coordinate origin, Coordinate destination, TravelMode travelMode = TravelMode.Driving);

        /// <summary>
        /// Gets a <see cref="Directions"/> object for the driving path between two addresses.
        /// </summary>
        /// <param name="originAddress">A <see cref="string"/> object representing the origin address.</param>
        /// <param name="destinationAddress">A <see cref="string"/> object representing the destination address.</param>
        /// /// <param name="travelMode">A <see cref="TravelMode"/> enum value corresponding to the desired mode of travel.</param>
        /// <returns>A <see cref="Directions"/> object, or null if no path exists.</returns>
        Directions GetDirections(string originAddress, string destinationAddress, TravelMode travelMode = TravelMode.Driving);

        /// <summary>
        /// Gets a <see cref="Directions"/> object for the driving path between an origin coordinate and a destination address.
        /// </summary>
        /// <param name="origin">A <see cref="Coordinate"/> object representing the origin location.</param>
        /// <param name="destinationAddress">A <see cref="string"/> object representing the destination address.</param>
        /// /// <param name="travelMode">A <see cref="TravelMode"/> enum value corresponding to the desired mode of travel.</param>
        /// <returns>A <see cref="Directions"/> object, or null if no path exists.</returns>
        Directions GetDirections(Coordinate origin, string destinationAddress, TravelMode travelMode = TravelMode.Driving);

        /// <summary>
        /// Gets a <see cref="Directions"/> object for the driving path between an origin address and a destination coordinate.
        /// </summary>
        /// <param name="originAddress">A <see cref="string"/> object representing the origin address.</param>
        /// <param name="destination">A <see cref="Coordinate"/> object representing the destination location.</param>
        /// /// <param name="travelMode">A <see cref="TravelMode"/> enum value corresponding to the desired mode of travel.</param>
        /// <returns>A <see cref="Directions"/> object, or null if no path exists.</returns>
        Directions GetDirections(string originAddress, Coordinate destination, TravelMode travelMode = TravelMode.Driving);
    }
}
