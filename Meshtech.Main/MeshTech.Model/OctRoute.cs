﻿using System;
using System.Globalization;
using System.Text;

namespace MeshTech.Model
{
    public class OctRoute
    {
        private readonly byte[] values = new byte[16];
        public const int OctCapacity = 8;
        public const int MacAddressCapacity = 12;

        private OctRoute()
        {

        }

        public const int MaxCellValue = 7;

        public static OctRoute Parse(string hexRoute)
        {
            var route = long.Parse(hexRoute, NumberStyles.HexNumber);
            var result = new OctRoute();

            int index = result.values.Length;
            do
            {
                result.values[index - 1] = Convert.ToByte(route % OctCapacity);
                index--;
                route = route / OctCapacity;
            }
            while (route > 0);

            return result;
        }

        public byte this[int index]    
        {
            get
            {
                var result = values[index];
                return result;
            }
        }

        public int Length
        {
            get { return values.Length; }
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

        public string ToHexString()
        {
            var stringValue = ToString();
            var longValue = Convert.ToInt64(stringValue, OctCapacity);
            var result = longValue.ToString("X");
            while (result.Length < MacAddressCapacity )
            {
                result = "0" + result;
            }
            return result;
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
