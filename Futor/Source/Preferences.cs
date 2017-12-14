using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Futor
{
    [Serializable]
    public class Preferences
    {
        static Preferences _instance;

        public static Preferences Instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        _instance = XmlSerializer<Preferences>.Deserialize(PreferencesPath);
                    }
                    catch
                    {
                        _instance = new Preferences();
                    }
                }

                return _instance;
            }
        }

        Preferences()
        {
        }
        
        public void Save()
        {
            XmlSerializer<Preferences>.Serialize(this, PreferencesPath);
        }

        static string PreferencesPath
        {
            get { return string.Format("{0}\\preferences.xml", Application.LocalUserAppDataPath); }
        }

        // Preferences ///////////////////////////////////////////////////////////////////////////////////////////////////////

        public class PluginInfo
        {
            public class BankInfo
            {
                public string Path;
            }

            public string Path;
            public bool IsBypass;
            public List<BankInfo> BankInfos = new List<BankInfo>();
        }

        public string LastPluginPath;

        public string InputDeviceName;
        public string OutputDeviceName;

        public List<PluginInfo> PluginInfos = new List<PluginInfo>();
    }
}
