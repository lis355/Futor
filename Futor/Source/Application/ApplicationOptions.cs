namespace Futor
{
    public class ApplicationOptions
    {
        const string _kPreferencesPath = "/preferences.xml";

        public bool HasAutorun
        {
            get
            {
                return Preferences<PreferencesDescriptor>.Instance.HasAutorun;
            }
            set
            {
                if (Preferences<PreferencesDescriptor>.Instance.HasAutorun == value)
                    return;

                Preferences<PreferencesDescriptor>.Instance.HasAutorun = value;

                if (value)
                    AutorunProvider.AddToStartup();
                else
                    AutorunProvider.RemoveFromStartup();
            }
        }

        public bool IsBypassAll
        {
            get
            {
                return Preferences<PreferencesDescriptor>.Instance.IsBypassAll;
            }
            set
            {
                if (Preferences<PreferencesDescriptor>.Instance.IsBypassAll == value)
                    return;
                
                Preferences<PreferencesDescriptor>.Instance.IsBypassAll = value;
            }
        }

        public void Load()
        {
            Preferences<PreferencesDescriptor>.Manager.Load(DataPathProvider.Path(_kPreferencesPath));
            
            ProcessAutorun();
        }

        public void Save()
        {
            Preferences<PreferencesDescriptor>.Manager.Save();
        }

        void ProcessAutorun()
        {
            var preferencesHasAutorun = Preferences<PreferencesDescriptor>.Instance.HasAutorun;
            if (preferencesHasAutorun != AutorunProvider.HasSturtup())
                HasAutorun = preferencesHasAutorun;
        }
    }
}
