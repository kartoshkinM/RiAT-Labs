using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Serializer
{
    public class SerializeXML : ISerialize
    {
        public T Deserialize<T>(string value)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var sr = new StringReader(value))
            {
                using (var xmlReader = XmlReader.Create(sr))
                {
                    return (T) serializer.Deserialize(xmlReader);
                }
            }
        }

        public string Serialize<T>(T value)
        {
            var serializer = new XmlSerializer(typeof(T));
            var namespaces = new XmlSerializerNamespaces(new [] { XmlQualifiedName.Empty, });
            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            using (var sw = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(sw, settings))
                {
                    serializer.Serialize(xmlWriter, value, namespaces);
                    return sw.ToString();
                }
            }
        }
    }
}
