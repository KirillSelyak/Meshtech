using System;
using System.Globalization;
using System.Text;

namespace MeshTech.Model
{
    public class OctRoute
    {
        private readonly byte[] values = new byte[16];

        private OctRoute()
        {

        }

        public static OctRoute Parse(string hexRoute)
        {
            var route = long.Parse(hexRoute, NumberStyles.HexNumber);
            var result = new OctRoute();

            int index = result.values.Length;
            do
            {
                result.values[index - 1] = Convert.ToByte(route % 8);
                index--;
                route = route / 8;
            }
            while (route > 0);

            return result;
        }

        public byte GetValue(int index)
        {
            var result = values[index];
            return result;
        }

        public OctRoute Insert(int index, byte value)
        {
            OctRoute result = Clone();
            result.values[index] = value;
            return result;
        }

        private OctRoute Clone()
        {
            var result = new OctRoute();
            for (int i = 0; i < values.Length; i++)
            {
                result.values[i] = values[i];
            }
            return result;
        }

        public string StringMask
        {
            get
            {
                var builder = new StringBuilder();
                for (int i = 0; i <= values.Length - 1; i++)
                {
                    builder.Append(values[i]);
                }
                var longValue = Convert.ToInt64(builder.ToString(), 8);
                var result = longValue.ToString("X");
                return result;
            }
        }

        public override int GetHashCode()
        {
            const int octCapacity = 8;
            int result = 0;
            for (int i = 0; i < 3; i++)
            {
                result = result * octCapacity + values[i];
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            var route = obj as OctRoute;
            if (route == null)
                return false;
            if (ReferenceEquals(this, route))
                return true;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] != route.values[i])
                    return false;
            }
            return true;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var item in values)
            {
                builder.Append(item);
            }
            var result = builder.ToString();
            return result;
        }

    }
}
