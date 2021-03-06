﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MeshTech.Model.Text;

namespace MeshTech.Model.IO
{
    public class StreamableBeaconsEnumerator : IEnumerator<Beacon>
    {
        private Beacon currentbeacon;
        private readonly StreamReader stream;
        private readonly IBeaconParser parser;

        public StreamableBeaconsEnumerator(StreamReader stream, IBeaconParser parser)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            if (parser == null)
                throw new ArgumentNullException(nameof(parser));

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
                return true;
            }
            return false;
        }

        public void Reset()
        {
            stream?.BaseStream
                .Seek(0, SeekOrigin.Begin);
            currentbeacon = null;
        }

        public void Dispose()
        {
            stream?.Dispose();
            currentbeacon = null;
        }

    }
}
