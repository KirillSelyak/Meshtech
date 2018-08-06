using System;
using MeshTech.Model;
using MeshTech.Model.IO;
using MeshTech.Model.Network;

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
                var beacons = BootStrapper.CreateEnumerableBeacons(fileStreamReader);
                var treeConstructor = BootStrapper.CreateTreeConstructor();
                var trees = treeConstructor.Construct(beacons);
                foreach (var tree in trees)
                {
                    PrintTree(tree);
                }
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

        private static void PrintTree(TreeNode root)
        {
            Console.WriteLine($"{root.Beacon.Route} - {root.Beacon.MacAddress}");
            foreach (var currentNode in root)
            {
                PrintTree(currentNode);
            }
        }

    }
}
