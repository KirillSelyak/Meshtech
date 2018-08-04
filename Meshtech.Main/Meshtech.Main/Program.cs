using System;
using System.Linq;
using MeshTech.Model;
using MeshTech.Model.IO;
using MeshTech.Model.IO.System;
using MeshTech.Model.Text;

namespace Meshtech.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            const string fileName = @"C:\Users\kiril\Desktop\Test\Test\rawio\rawio.log";

            var root = CreateRoot();
            var reader = CreateReader(fileName);

            foreach (var beacon in reader.Where(o => !string.IsNullOrEmpty(o.MacAddress)))
            {
                var currentNode = root;
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
                            targetBeacon.MacAddress = (j == i)
                                ? beacon.MacAddress
                                : string.Empty;
                            currentNode = currentNode.AddOrUpdate(currentValue, targetBeacon);
                        }
                        break;
                    }
                }
            }
            Print(root);
            Console.ReadLine();
        }

        public static void Print(RouteNode currentNode)
        {
            Console.WriteLine($"{currentNode.Beacon.Route.StringMask} - {currentNode.Beacon.MacAddress}");
            for (int i = 0; i < currentNode.ChildRouteNodes.Length - 1; i++)
            {
                var targetNode = currentNode.ChildRouteNodes[i];
                if (currentNode.ChildRouteNodes[i] != null)
                {
                    Print(targetNode);
                }
            }
        }

        private static StreamebleBeacons CreateReader(string path)
        {
            var fileStreamFactory = new FileStreamReaderFactory(path);
            var beaconParser = new BeaconParser();
            var beaconEnumeratorFactory = new BeaconEnumeratorFactory(beaconParser);
            var result = new StreamebleBeacons(beaconEnumeratorFactory, fileStreamFactory);
            return result;
        }

        private static RouteNode CreateRoot()
        {
            var beacond = new Beacon()
            {
                Route = Route.Parse("FFFFFFFFFFF8")
            };
            var result = new RouteNode(beacond);
            return result;
        }
    }
}
