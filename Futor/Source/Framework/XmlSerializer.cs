using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Futor
{
    public class XmlSerializer<T>
    {
        public static void Serialize(T obj, string filePath)
        {
            var xmlSerializerNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            var serializer = new XmlSerializer(typeof(T));
            using (TextWriter writer = new StreamWriter(filePath))
            {
                var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true });

                serializer.Serialize(xmlWriter, obj, xmlSerializerNamespaces);
            }
        }

        public static T Deserialize(string filePath)
        {
            var deserializer = new XmlSerializer(typeof(T));
            using (var reader = new StreamReader(filePath))
                return (T)deserializer.Deserialize(reader);
        }
    }
}
