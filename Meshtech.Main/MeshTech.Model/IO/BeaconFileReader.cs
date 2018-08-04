using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace MeshTech.Model.IO
{
    public class BeaconFileReader : IEnumerable<Beacon>
    {
        private string path;
        private IBeaconParser beaconParser;

        public BeaconFileReader(string path, IBeaconParser beaconParser)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            this.path = path;
            this.beaconParser = beaconParser;
        }

        public IEnumerator<Beacon> GetEnumerator()
        {
            var streamReader = new StreamReader(path);
            var result = new BeaconFileEnumerator(streamReader, beaconParser);
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
