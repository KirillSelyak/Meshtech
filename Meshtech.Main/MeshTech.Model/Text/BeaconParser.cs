using System;

namespace MeshTech.Model.Text
{
    public class BeaconParser : IBeaconParser
    {
        private const int routeIndex = 5;
        private const int macAddressIndex = 6;
        private const int gatewayIndex = 4;
        private readonly PairParser pairParser = new PairParser(':');

        public Beacon Parse(string line)
        {
            try
            {
                var result = new Beacon();
                var information = line.Split('|');

                var routePair = ParsePair(information, routeIndex);
                result.Route = OctRoute.Parse(routePair.Value);

                var macAddressPair = ParsePair(information, macAddressIndex);
                if (macAddressPair.Key == "BM")
                    result.MacAddress = macAddressPair.Value;

                var gatewayPair = ParsePair(information, gatewayIndex);
                result.Gateway = gatewayPair.Value;
                return result;
            }
            catch (InvalidOperationException)
            {
                return Beacon.InvalidBeacon;
            }
            catch (IndexOutOfRangeException)
            {
                return Beacon.InvalidBeacon;
            }
        }

        private Pair ParsePair(string[] source, int index)
        {
            var item = source[index];
            var result = pairParser.Parse(item);
            return result;
        }

    }
}
