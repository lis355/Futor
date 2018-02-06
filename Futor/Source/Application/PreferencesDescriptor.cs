using System.Collections.Generic;

namespace Futor
{
    public class PreferencesDescriptor
    {
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

        public bool HasAutorun;
        public string LastPluginPath;
        
        public string InputDeviceName;
        public string OutputDeviceName;

        public int LatencyMilliseconds;

        public List<PluginInfo> PluginInfos = new List<PluginInfo>();
    }
}
