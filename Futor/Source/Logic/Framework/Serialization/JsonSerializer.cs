using System.IO;
using System.Web.Script.Serialization;

namespace Futor
{
    public class JsonSerializer<T> : Serializer<T>
    {
        public override void Serialize(T obj, string filePath)
        {
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(obj);
            File.WriteAllText(filePath, serializedResult);
        }

        public override T Deserialize(string filePath)
        {
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Deserialize<T>(File.ReadAllText(filePath));
            return serializedResult;
        }
    }
}
