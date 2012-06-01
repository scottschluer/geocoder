<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Geocoder.Example.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-bottom:10px;">
            Geocode type: <asp:DropDownList runat="server" ID="drpGeocodeType" OnSelectedIndexChanged="drpGeoCodeType_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem Value="">-- Select --</asp:ListItem>
                <asp:ListItem>Get Lat/Long From Address</asp:ListItem>
                <asp:ListItem>Get Address From Lat/Long</asp:ListItem>
                <asp:ListItem>Get Driving Directions</asp:ListItem>
            </asp:DropDownList>
        </div>
        
        <asp:Panel runat="server" ID="pnlGeocode" Visible="False">
            Enter an address and click 'Search' to retrieve the coordinates for that location.<br/>
            <b>Address</b> <asp:TextBox runat="server" ID="Address" />&nbsp;
            <asp:Button runat="server" ID="btnGetLatLong" OnClick="btnGetLatLong_Click" Text="Search"/>
        </asp:Panel>
        
        <asp:Panel runat="server" ID="pnlReverseGeocode" Visible="False">
            Enter a latitude and longitude and click 'Search' to retrieve the address at that location.<br/>
            This demo does not validate your input, so be sure to enter appropriate values.<br/>
            <b>Latitude</b> <asp:TextBox runat="server" ID="Latitude" /><br/>
            <b>Longitude</b> <asp:TextBox runat="server" ID="Longitude" /><br/>
            <asp:Button runat="server" ID="btnGetAddress" OnClick="btnGetAddress_Click" Text="Search"/>
        </asp:Panel>
        
        <asp:Panel runat="server" ID="pnlDirections" Visible="False">
            Enter an origin and destination address, choose a travel mode and click 'Search' to get directions between the two points.<br/>
            For the purposes of this demo, you must enter an address, not coordinate pairs although the API does support coordinate pairs.<br/>
            <b>Travel Mode</b> <asp:DropDownList runat="server" ID="drpTravelMode">
                <asp:ListItem>Driving</asp:ListItem>
                <asp:ListItem>Walking</asp:ListItem>
                <asp:ListItem>Bicycling</asp:ListItem>
            </asp:DropDownList><br/>
            <b>Origin</b> <asp:TextBox runat="server" ID="Origin" /><br/>
            <b>Destination</b> <asp:TextBox runat="server" ID="Destination" /><br/>
            <asp:Button runat="server" ID="btnGetDirections" OnClick="btnGetDirections_Click" Text="Search"/>
        </asp:Panel>
        
        <asp:Label runat="server" ID="lblResult" EnableViewState="False" />
        
        <asp:GridView runat="server" ID="gvDirections" EnableViewState="False" Visible="False" AutoGenerateColumns="False" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="Direction" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Server.HtmlDecode((string)Eval("Instruction")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Distance" DataField="Distance" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
