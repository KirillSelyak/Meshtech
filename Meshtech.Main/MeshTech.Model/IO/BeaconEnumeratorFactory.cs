using System;
using System.Collections.Generic;
using System.IO;
using MeshTech.Model.Text;

namespace MeshTech.Model.IO
{
    public class BeaconEnumeratorFactory: IBeaconEnumeratorFactory
    {
        private readonly IBeaconParser beaconParser;

        public BeaconEnumeratorFactory(IBeaconParser beaconParser)
        {
            if (beaconParser == null)
                throw new ArgumentNullException(nameof(beaconParser));

            this.beaconParser = beaconParser;
        }

        public IEnumerator<Beacon> CreateBeaconEnumerator(StreamReader streamReader)
        {
            var result = new StreamableBeaconsEnumerator(streamReader, beaconParser);
            return result;
        }
    }
}
