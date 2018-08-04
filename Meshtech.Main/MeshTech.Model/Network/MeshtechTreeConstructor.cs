using System;
using System.Linq;
using MeshTech.Model.IO;

namespace MeshTech.Model.Network
{
    public class MeshtechTreeConstructor
    {
      
        public RouteNode Construct(StreamebleBeacons streamebleBeacons)
        {
            if (streamebleBeacons == null)
                throw new ArgumentNullException(nameof(streamebleBeacons));

            var result = CreateRoot();
            foreach (var beacon in streamebleBeacons.Where(o => !string.IsNullOrEmpty(o.MacAddress)))
            {
                var currentNode = result;
                for (int i = 14; i >= 0; i--)
                {
                    var shift = beacon.Route.OctMask[i];
                    if (shift != 7)
                    {
                        for (int j = 0; j <= i; j++)
                        {
                            var targetBeacon = (Beacon)currentNode.Beacon.Clone();
                            var currentValue = beacon.Route.OctMask[j];
                            targetBeacon.Route.OctMask[j] = currentValue;
                            targetBeacon.MacAddress = j == i
                                ? beacon.MacAddress
                                : string.Empty;
                            currentNode = currentNode.AddOrUpdate(currentValue, targetBeacon);
                        }
                        break;
                    }
                }
            }
            return result;
        }

        private static RouteNode CreateRoot()
        {
            var beacond = new Beacon
            {
                Route = Route.Parse("FFFFFFFFFFF8")
            };
            var result = new RouteNode(beacond);
            return result;
        }
    }
}
