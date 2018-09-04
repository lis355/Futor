using System;
using System.IO;

namespace Futor
{
    public class Preferences<T> where T : new()
    {
        IPreferencePathProvider _preferencePathProvider;
        bool _isLoaded;

        public T Object { get; private set; }

        public event Action OnLoaded;
        public event Action OnReloaded;
        public event Action OnSaved;

        public void Load(IPreferencePathProvider preferencePathProvider)
        {
            _preferencePathProvider = preferencePathProvider;

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
            var filePath = _preferencePathProvider.Path;
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

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
            serializer.Serialize(Object, _preferencePathProvider.Path);

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
                Object = serializer.Deserialize(_preferencePathProvider.Path);
            }
            catch
            {
                Object = new T();
            }
        }
    }
}
