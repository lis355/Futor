using System;
using System.IO;

namespace Futor
{
    public class Preferences<T> where T : new()
    {
        static Preferences<T> _instance;

        public static Preferences<T> Manager => _instance ?? (_instance = new Preferences<T>());

        public static T Instance
        {
            get
            {
                if (Manager._preferencesObject == null)
                    Manager.Reload();

                return Manager._preferencesObject;
            }
        }

        T _preferencesObject;
        string _filePath;
        bool _isLoaded;

        Preferences()
        {
        }

        public void Load(string filePath)
        {
            _filePath = filePath;

            TryParsePreferencesFile();

            _isLoaded = true;
        }

        public void Reload()
        {
            if (!File.Exists(_filePath))
                throw new FileNotFoundException(_filePath);

            TryParsePreferencesFile();
        }

        public void Save()
        {
            if (!_isLoaded)
                throw new Exception("Preferences is not loaded.");

            var serializer = new XmlSerializer<T>();
            serializer.Serialize(_instance._preferencesObject, _filePath);
        }

        void TryParsePreferencesFile()
        {
            try
            {
                var serializer = new XmlSerializer<T>();
                _preferencesObject = serializer.Deserialize(_filePath + "111");
            }
            catch
            {
                _preferencesObject = new T();
            }
        }
    }
}
