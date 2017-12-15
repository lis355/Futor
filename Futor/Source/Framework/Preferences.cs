namespace Futor
{
    public class Preferences<T> where T : new()
    {
        static Preferences<T> _instance;

        public static Preferences<T> Manager
        {
            get
            {
                if (_instance == null)
                    _instance = new Preferences<T>();

                return _instance;
            }
        }

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

        Preferences()
        {
        }

        public void Load(string filePath)
        {
            _filePath = filePath;

            try
            {
                _preferencesObject = XmlSerializer<T>.Deserialize(_filePath);
            }
            catch
            {
                _preferencesObject = new T();
            }
        }

        public void Reload()
        {
            try
            {
                _preferencesObject = XmlSerializer<T>.Deserialize(_filePath);
            }
            catch
            {
                _preferencesObject = new T();
            }
        }

        public void Save()
        {
            XmlSerializer<T>.Serialize(_instance._preferencesObject, _filePath);
        }
    }
}
