using System;
using System.Linq;
using System.Xml.Linq;
using Geocoder.Enums;
using Geocoder.Interfaces;

namespace Geocoder
{
    public class GoogleGeocoder : IGeocoder
    {
        const string API_REVERSE_GEOCODE = "maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false";
        const string API_GEOCODE = "maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false";
        const string API_DIRECTIONS = "maps.googleapis.com/maps/api/directions/xml?origin={0}&destination={1}&mode={2}&sensor=false";

        /// <summary>
        /// Gets a value indicating whether to send API requests via HTTPS.
        /// </summary>
        /// <value>
        ///   <c>true</c> if sending API requests via HTTPS; otherwise, <c>false</c>.
        /// </value>
        public bool IsSSL { get; private set; }


        private string Protocol
        {
            get { return IsSSL ? "https://" : "http://"; }
        }

        protected string ApiReverseGeoCode
        {
            get { return Protocol + API_REVERSE_GEOCODE; }
        }

        protected string ApiGeoCode
        {
            get { return Protocol + API_GEOCODE; }
        }

        protected string ApiDirections
        {
            get { return Protocol + API_DIRECTIONS; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleGeocoder"/> class.
        /// </summary>
        /// <param name="isSSL">Indicates whether to send API requests via HTTPS.</param>
        public GoogleGeocoder(bool isSSL)
        {
            IsSSL = isSSL;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleGeocoder"/> class and defaults to transmission over HTTP. 
        /// </summary>
        public GoogleGeocoder() : this(false) { }

        public Address GetAddressFromLatLong(double latitude, double longitude)
        {
            if (!CoordinateValidator.Validate(latitude, longitude))
                throw new ArgumentException("Invalid coordinate supplied.");

            XDocument doc = XDocument.Load(String.Format(ApiReverseGeoCode, latitude, longitude));

            var result = doc.Descendants("result").First();
            return result != null
                       ? new Address
                       {
                           FormattedAddress = result.Descendants("formatted_address").First().Value,
                           StreetNumber = result.Descendants("address_component").First(x => x.Descendants("type").Any(y => y.Value == "street_number")).Descendants("short_name").First().Value,
                           StreetName = result.Descendants("address_component").First(x => x.Descendants("type").Any(y => y.Value == "route")).Descendants("short_name").First().Value,
                           City = result.Descendants("address_component").First(x => x.Descendants("type").Any(y => y.Value == "locality")).Descendants("short_name").First().Value,
                           State = result.Descendants("address_component").First(x => x.Descendants("type").Any(y => y.Value == "administrative_area_level_1")).Descendants("short_name").First().Value,
                           ZipCode = result.Descendants("address_component").First(x => x.Descendants("type").Any(y => y.Value == "postal_code")).Descendants("short_name").First().Value,
                           Country = result.Descendants("address_component").First(x => x.Descendants("type").Any(y => y.Value == "country")).Descendants("long_name").First().Value,
                           County = result.Descendants("address_component").First(x => x.Descendants("type").Any(y => y.Value == "administrative_area_level_2")).Descendants("short_name").First().Value
                       }
                       : null;
        }

        public Coordinate GetLatLongFromAddress(string address)
        {
            XDocument doc = XDocument.Load(String.Format(ApiGeoCode, address));

            var result = doc.Descendants("result").Descendants("geometry").Descendants("location").First();
            return result != null
                       ? new Coordinate
                       {
                           Latitude = Double.Parse((result.Descendants("lat").First().Value)),
                           Longitude = Double.Parse((result.Descendants("lng").First().Value))
                       }
                       : null;
        }

        public Directions GetDirections(Coordinate origin, Coordinate destination, TravelMode travelMode = TravelMode.Driving)
        {
            if (!CoordinateValidator.Validate(origin.Latitude, origin.Longitude))
                throw new ArgumentException("Invalid origin coordinate supplied.");

            if (!CoordinateValidator.Validate(destination.Latitude, destination.Longitude))
                throw new ArgumentException("Invalid destination coordinate supplied.");

            return GetDirections(XDocument.Load(String.Format(ApiDirections,
                        String.Format("{0},{1}", origin.Latitude, origin.Longitude),
                        String.Format("{0},{1}", destination.Latitude, destination.Longitude),
                        travelMode.ToString().ToLower())));
        }

        public Directions GetDirections(string originAddress, string destinationAddress, TravelMode travelMode = TravelMode.Driving)
        {
            return GetDirections(XDocument.Load(String.Format(ApiDirections, originAddress, destinationAddress, travelMode.ToString().ToLower())));
        }

        public Directions GetDirections(Coordinate origin, string destinationAddress, TravelMode travelMode = TravelMode.Driving)
        {
            return GetDirections(XDocument.Load(String.Format(ApiDirections, String.Format("{0},{1}", origin.Latitude, origin.Longitude), destinationAddress, travelMode.ToString().ToLower())));
        }

        public Directions GetDirections(string originAddress, Coordinate destination, TravelMode travelMode = TravelMode.Driving)
        {
            return GetDirections(XDocument.Load(String.Format(ApiDirections, originAddress, String.Format("{0},{1}", destination.Latitude, destination.Longitude), travelMode.ToString().ToLower())));
        }

        private Directions GetDirections(XDocument doc)
        {
            Directions directions = new Directions();

            string status = doc.Descendants("DirectionsResponse").Descendants("status").First().Value;

            if (String.IsNullOrEmpty(status) || status != "OK")
                return null;

            var leg = doc.Descendants("DirectionsResponse").Descendants("route").Descendants("leg");

            directions.Distance = leg.Descendants("distance").Descendants("text").LastOrDefault().Value;
            directions.Duration = leg.Descendants("duration").Descendants("text").LastOrDefault().Value;

            foreach (var step in leg.Descendants("step"))
                directions.Steps.Add(new DirectionStep
                {
                    Instruction = step.Element("html_instructions").Value,
                    Distance = step.Descendants("distance").First().Element("text").Value
                });


            return directions;
        }
    }
}
