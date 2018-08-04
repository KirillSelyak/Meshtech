using System;
using System.Globalization;
using System.Text;

namespace MeshTech.Model.IO
{
    public class Route : ICloneable
    {
        private byte[] octMask;

        public static Route Parse(string stringMask)
        {
            var intValue = long.Parse(stringMask, NumberStyles.HexNumber);
            var octMask = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int index = 16;
            do
            {
                octMask[index - 1] = Convert.ToByte(intValue % 8);
                index--;
                intValue = intValue / 8;
            }
            while (intValue > 0);
            var result = new Route()
            {
                OctMask = octMask
            };
            return result;
        }

        public object Clone()
        {
            var clonedOctMask = (byte[])octMask.Clone();
            var result = new Route();
            result.OctMask = clonedOctMask;
            return result;
        }

        public byte[] OctMask
        {
            get { return octMask; }
            private set { octMask = value; }
        }

        public string StringMask
        {
            get
            {
                var builder = new StringBuilder();
                for (int i = 0; i <= octMask.Length - 1; i++)
                {
                    builder.Append(octMask[i]);
                }
                var longValue = Convert.ToInt64(builder.ToString(), 8);
                var result = longValue.ToString("X");
                return result;
            }
        }

    }
}
