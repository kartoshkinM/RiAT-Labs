namespace Serializer
{
    public interface ISerialize
    {
        T Deserialize<T>(string value);
        string Serialize<T>(T value);
    }
}
