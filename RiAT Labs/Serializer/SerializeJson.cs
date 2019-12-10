using Newtonsoft.Json;

namespace Serializer
{
    public class SerializeJson : ISerialize
    {
        public T Deserialize<T>(string value)
            => JsonConvert.DeserializeObject<T>(value);

        public string Serialize<T>(T value)
            => JsonConvert.SerializeObject(value);
    }
}
