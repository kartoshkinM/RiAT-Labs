using System;

namespace Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            var serializationType = Console.ReadLine();
            var data = Console.ReadLine();
            Console.WriteLine(Serializer.Actions.DataTransform(serializationType, data));
            Console.ReadKey();
        }
    }
}
