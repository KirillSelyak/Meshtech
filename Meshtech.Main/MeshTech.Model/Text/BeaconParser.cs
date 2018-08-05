using System;

namespace MeshTech.Model.Text
{
    public class BeaconParser : IBeaconParser
    {
        private const int routeIndex = 5;
        private const int macAddressIndex = 6;

        public Beacon Parse(string line)
        {
            try
            {
                var result = new Beacon();
                var information = line.Split('|');

                var rawRoute = information[routeIndex];
                result.Route = ParseRoute(rawRoute);

                if (information.Length > macAddressIndex)
                {
                    var rawMacAddress = information[macAddressIndex];
                    result.MacAddress = ParseMacAddess(rawMacAddress);
                }
                return result;
            }
            catch (IndexOutOfRangeException)
            {
                return Beacon.InvalidBeacon;
            }
        }

        private OctRoute ParseRoute(string rawRoute)
        {
            var number = rawRoute.Substring(3);
            var route = OctRoute.Parse(number);
            return route;
        }

        private string ParseMacAddess(string rawMacAddress)
        {
            var result = string.Empty;
            if (rawMacAddress.StartsWith("BM:"))
            {
                result = rawMacAddress.Substring(3);
            }
            return result;
        }
    }
}
