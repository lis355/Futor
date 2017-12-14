using System.IO;
using System.Xml.Serialization;

namespace Futor
{
    public class XmlSerializer<T>
    {
        public static void Serialize(T obj, string filePath)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (TextWriter writer = new StreamWriter(filePath))
                serializer.Serialize(writer, obj);
        }

        public static T Deserialize(string filePath)
        {
            var deserializer = new XmlSerializer(typeof(T));
            using (var reader = new StreamReader(filePath))
                return (T)deserializer.Deserialize(reader);
        }
    }
}
