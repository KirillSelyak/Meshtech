using System;

namespace MeshTech.Model.Text
{
    public class PairParser
    {
        private readonly char delimeter;

        public PairParser(char delimeter)
        {
            this.delimeter = delimeter;
        }

        public Pair Parse(string input)
        {
            var items = input.Split(delimeter);
            if (items.Length != 2)
                throw new InvalidOperationException($"input {input} is not pair splitted by - {delimeter}.");
            var result = new Pair
            {
                Key = items[0],
                Value = items[1]
            };
            return result;
        }

    }
}
