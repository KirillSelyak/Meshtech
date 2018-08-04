using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace MeshTech.Model.IO
{
    public class BeaconFileEnumerator : IEnumerator<Beacon>
    {
        private Beacon currentbeacon;
        private StreamReader stream;
        private IBeaconParser parser;

        public BeaconFileEnumerator(StreamReader stream, IBeaconParser parser)
        {
            this.stream = stream;
            this.parser = parser;
        }

        public Beacon Current => currentbeacon;

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            var isLineAvailable = stream.Peek() >= 0;

            if (isLineAvailable)
            {
                var line = stream.ReadLine();
                currentbeacon = parser.Parse(line);
                return isLineAvailable;
            }
            return isLineAvailable;
        }

        public void Reset()
        {
            stream?.BaseStream.Seek(0, SeekOrigin.Begin);
            currentbeacon = null;
        }

        public void Dispose()
        {
            stream?.Dispose();
            currentbeacon = null;
        }

    }
}
