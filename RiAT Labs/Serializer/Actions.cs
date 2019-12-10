using System;
using System.Linq;

namespace Serializer
{
    public class Actions
    {
        public static string DataTransform(string serializationType, string data)
        {
            ISerialize serializer = GetSerializer(serializationType);
            var input = serializer.Deserialize<Input>(data);
            var output = serializer.Serialize(GetOutput(input));
            return output;
        }

        private static ISerialize GetSerializer(string serializationType)
        {
            switch (serializationType.ToLower())
            {
                case "json":
                    return new SerializeJson();
                case "xml":
                    return new SerializeXML();
                default:
                    throw new Exception("Неверный тип объекта");
            }
        }

        public static Output GetOutput(Input inputData)
        {
            return new Output()
            {
                SumResult = inputData.Sums.Sum() * inputData.K,
                MulResult = inputData.Muls.Aggregate((x, y) => x * y),
                SortedInputs = inputData.Sums.Concat(inputData.Muls.Select(e
                    => (decimal)e)).OrderBy(e => e).ToArray()
            };
        }
    }
}
