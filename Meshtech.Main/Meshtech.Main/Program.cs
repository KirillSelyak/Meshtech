﻿using System;
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
                PrintHeader(fileName);

                var fileStreamReader = CreateFileStreamReaderFactory(fileName);
                var beacons = BootStrapper.CreateEnumerableBeacons(fileStreamReader);
                var treeConstructor = BootStrapper.CreateTreeConstructor();
                var trees = treeConstructor.Construct(beacons);
                foreach (var tree in trees)
                {
                    PrintTree(tree);
                }
            }

            PrintFooter();
        }

        private static void PrintFooter()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to exit ...");
            Console.ReadLine();
        }

        private static void PrintHeader(string fileName)
        {
            Console.WriteLine();
            Console.WriteLine($"Network(s) from log file -'{fileName}':");
        }

        private static IStreamReaderFactory CreateFileStreamReaderFactory(string path)
        {
            var result = new FileStreamReaderFactory(path);
            return result;
        }

        private static void PrintTree(TreeNode root)
        {
            Console.WriteLine($"{root.Beacon.Route.ToHexString()} - {root.Beacon.MacAddress}");
            foreach (var currentNode in root)
            {
                PrintTree(currentNode);
            }
        }

    }
}
