using System;
using Geocoder.Enums;
using Geocoder.Interfaces;

namespace Geocoder.Example
{
    public partial class Default : System.Web.UI.Page
    {
        private readonly IGeocoder _locationService = new GoogleGeocoder();

        protected void btnGetLatLong_Click(object sender, EventArgs e)
        {
            Coordinate coordinate = _locationService.GetLatLongFromAddress(Address.Text);

            if (coordinate != null)
            {
                lblResult.Text = String.Format("{0} is located at {1}, {2}",
                    Address.Text,
                    coordinate.Latitude.ToString(),
                    coordinate.Longitude.ToString());
            }
            else
            {
                lblResult.Text = "Address not found";
            }
        }

        protected void btnGetAddress_Click(object sender, EventArgs e)
        {
            Address address = _locationService.GetAddressFromLatLong(Convert.ToDouble(Latitude.Text), Convert.ToDouble(Longitude.Text));

            if (address != null)
            {
                lblResult.Text = String.Format("{0}, {1} maps to the following address: {2}",
                    Latitude.Text,
                    Longitude.Text,
                    address.FormattedAddress);
            }
            else
            {
                lblResult.Text = "No address found at that location.";
            }
        }

        protected void btnGetDirections_Click(object sender, EventArgs e)
        {
            switch (drpTravelMode.SelectedIndex)
            {
                case 1:
                    gvDirections.DataSource = _locationService.GetDirections(Origin.Text, Destination.Text, TravelMode.Walking).Steps;
                    break;
                case 2:
                    gvDirections.DataSource = _locationService.GetDirections(Origin.Text, Destination.Text, TravelMode.Bicycling).Steps;
                    break;
                default:
                    gvDirections.DataSource = _locationService.GetDirections(Origin.Text, Destination.Text).Steps;
                    break;
            }

            gvDirections.DataBind();
            gvDirections.Visible = true;
        }

        protected void drpGeoCodeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (drpGeocodeType.SelectedIndex)
            {
                case 1:
                    pnlGeocode.Visible = true;
                    pnlDirections.Visible = false;
                    pnlReverseGeocode.Visible = false;
                    break;
                case 2:
                    pnlGeocode.Visible = false;
                    pnlDirections.Visible = false;
                    pnlReverseGeocode.Visible = true;
                    break;
                case 3:
                    pnlGeocode.Visible = false;
                    pnlDirections.Visible = true;
                    pnlReverseGeocode.Visible = false;
                    break;
                default:
                    pnlGeocode.Visible = false;
                    pnlDirections.Visible = false;
                    pnlReverseGeocode.Visible = false;
                    break;
            }
        }
    }
}