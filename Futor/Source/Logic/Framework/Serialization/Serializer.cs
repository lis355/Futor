namespace Futor
{
    public abstract class Serializer<T>
    {
        public abstract void Serialize(T obj, string filePath);
        public abstract T Deserialize(string filePath);
    }
}
