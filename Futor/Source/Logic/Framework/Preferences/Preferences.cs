using System;
using System.IO;

namespace Futor
{
    public class Preferences<T> where T : new()
    {
        string _filePath;
        bool _isLoaded;

        public T Object { get; private set; }

        public event Action OnLoaded;
        public event Action OnReloaded;
        public event Action OnSaved;

        public void Load(string filePath)
        {
            _filePath = filePath;

            TryParsePreferencesFile();

            _isLoaded = true;

            Loaded();
        }

        void Loaded()
        {
            OnLoaded?.Invoke();
        }

        public void Reload()
        {
            if (!File.Exists(_filePath))
                throw new FileNotFoundException(_filePath);

            TryParsePreferencesFile();

            Reloaded();
        }

        void Reloaded()
        {
            OnReloaded?.Invoke();

            Loaded();
        }

        public void Save()
        {
            if (!_isLoaded)
                throw new Exception("Preferences is not loaded.");

            var serializer = new XmlSerializer<T>();
            serializer.Serialize(Object, _filePath);

            Saved();
        }

        void Saved()
        {
            OnSaved?.Invoke();
        }

        void TryParsePreferencesFile()
        {
            try
            {
                var serializer = new XmlSerializer<T>();
                Object = serializer.Deserialize(_filePath);
            }
            catch
            {
                Object = new T();
            }
        }
    }
}
