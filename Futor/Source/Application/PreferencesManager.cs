using System;
using System.Windows.Forms;

namespace Futor
{
    [Serializable]
    public class PreferencesManager<T> where T : new()
    {
        static PreferencesManager<T> _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PreferencesManager<T>();

                    try
                    {
                        _instance.PreferencesObject = XmlSerializer<T>.Deserialize(PreferencesPath);
                    }
                    catch
                    {
                        _instance.PreferencesObject = new T();
                    }
                }

                return _instance.PreferencesObject;
            }
        }
        
        public T PreferencesObject { get; private set; }

        PreferencesManager()
        {
        }
        
        public void Save()
        {
            XmlSerializer<T>.Serialize(_instance.PreferencesObject, PreferencesPath);
        }

        static string PreferencesPath
        {
            get { return string.Format("{0}\\preferences.xml", Application.LocalUserAppDataPath); }
        }
    }
}
