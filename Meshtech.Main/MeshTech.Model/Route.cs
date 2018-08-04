using System;
using System.Globalization;
using System.Text;

namespace MeshTech.Model
{
    public class Route
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
            var result = new Route
            {
                octMask = octMask
            };
            return result;
        }

        public byte GetValue(int index)
        {
            var result = octMask[index];
            return result;
        }

        public Route Insert(int index, byte value)
        {
            var clonedOctMask = (byte[])octMask.Clone();
            var result = new Route
            {
                octMask = clonedOctMask
            };
            result.octMask[index] = value;
            return result;
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

        public string StringOctMask
        {
            get
            {
                var builder = new StringBuilder();
                foreach (var item in octMask)
                {
                    builder.Append(item);
                }
                var result = builder.ToString();
                return result;
            }
        }

    }
}
