namespace Futor
{
    public class ApplicationOptions
    {
        const string _kPreferencesPath = "/preferences.xml";

        PreferencesDescriptor Options => Preferences<PreferencesDescriptor>.Instance;

        public bool HasAutorun
        {
            get { return Options.HasAutorun; }
            set
            {
                if (Options.HasAutorun == value)
                    return;

                SetHasAutorun(value);
            }
        }

        public bool IsBypassAll
        {
            get { return Options.IsBypassAll; }
            set { Options.IsBypassAll = value; }
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
            if (HasAutorun != AutorunProvider.HasSturtup())
                SetHasAutorun(HasAutorun);
        }

        void SetHasAutorun(bool value)
        {
            Options.HasAutorun = value;

            if (value)
                AutorunProvider.AddToStartup();
            else
                AutorunProvider.RemoveFromStartup();
        }
    }
}
