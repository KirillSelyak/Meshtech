using System;
using MeshTech.Model;
using MeshTech.Model.IO;

namespace Meshtech.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var fileName in args)
            {
                PrintInformationHeaderForTree(fileName);

                var fileStreamReader = CreateFileStreamReaderFactory(fileName);
                var streamebleBeacons = BootStrapper.CreateStreamebleBeacons(fileStreamReader);
                var treeConstructor = BootStrapper.CreateTreeConstructor();
                var tree = treeConstructor.Construct(streamebleBeacons);
                PrintTree(tree);
            }

            PrintInformationFooter();
        }

        private static void PrintInformationFooter()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to exit ...");
            Console.ReadLine();
        }

        private static void PrintInformationHeaderForTree(string fileName)
        {
            Console.WriteLine($"Tree from log file -'{fileName}':");
            Console.WriteLine();
        }

        private static IStreamReaderFactory CreateFileStreamReaderFactory(string path)
        {
            var result = new FileStreamReaderFactory(path);
            return result;
        }

        private static void PrintTree(RouteNode root)
        {
            Console.WriteLine($"{root.Beacon.Route.StringMask} - {root.Beacon.MacAddress}");
            for (int i = 0; i < root.ChildRouteNodes.Length - 1; i++)
            {
                var targetNode = root.ChildRouteNodes[i];
                if (root.ChildRouteNodes[i] != null)
                {
                    PrintTree(targetNode);
                }
            }
        }

    }
}
