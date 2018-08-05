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
                for (int i = beacon.Route.Length - 1; i >= 0; i--)
                {
                    var shift = beacon.Route[i];
                    if (shift != OctRoute.MaxCellValue)
                    {
                        for (int j = 0; j <= i; j++)
                        {
                            var targetBeacon = new Beacon();
                            var currentValue = beacon.Route[j];
                            targetBeacon.Route = currentNode.Beacon.Route.Insert(j, currentValue);
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
                Route = OctRoute.Parse("FFFFFFFFFFF8")
            };
            var result = new RouteNode(beacond);
            return result;
        }
    }
}
