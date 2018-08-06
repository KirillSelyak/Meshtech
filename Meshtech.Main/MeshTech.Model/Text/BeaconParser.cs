using System;

namespace MeshTech.Model.Text
{
    public class BeaconParser : IBeaconParser
    {
        private const int routeIndex = 5;
        private const int macAddressIndex = 6;
        private const int gatewayIndex = 4;

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

                var rawGateway = information[gatewayIndex];
                result.Gateway = ParseGateway(rawGateway);
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
            const string prefix = "BM:";
            if (rawMacAddress.StartsWith(prefix))
            {
                result = rawMacAddress.Substring(prefix.Length);
            }
            return result;
        }

        private string ParseGateway(string rawGateway)
        {
            var result = rawGateway.Substring(3);
            return result;
        }

    }
}
