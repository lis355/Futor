using System;

namespace Futor
{
    public class PreferenceController
    {
        const string _kPreferencesPath = "preferences.xml";
        
        Preferences<PreferencesDescriptor> _manager;

        public bool HasAutorun
        {
            get => _manager.Object.HasAutorun;
            set
            {
                if (_manager.Object.HasAutorun == value)
                    return;

                _manager.Object.HasAutorun = value;

                SetAutorun(value);

                OnHasAutorunChanged?.Invoke();

                Save();
            }
        }

        public event Action OnHasAutorunChanged;

        public string InputDeviceName
        {
            get => _manager.Object.InputDeviceName;
            set => _manager.Object.InputDeviceName = value;
        }

        public string OutputDeviceName
        {
            get => _manager.Object.OutputDeviceName;
            set => _manager.Object.OutputDeviceName = value;
        }

        public int LatencyMilliseconds
        {
            get => _manager.Object.LatencyMilliseconds;
            set => _manager.Object.LatencyMilliseconds = value;
        }

        public int PitchFactor
        {
            get => _manager.Object.PitchFactor;
            set
            {
                if (_manager.Object.PitchFactor == value)
                    return;

                _manager.Object.PitchFactor = value;

                OnPitchFactorChanged?.Invoke();

                Save();
            }
        }

        public event Action OnPitchFactorChanged;

        public bool IsBypassAll
        {
            get => _manager.Object.IsBypassAll;
            set
            {
                if (_manager.Object.IsBypassAll == value)
                    return;

                _manager.Object.IsBypassAll = value;

                OnIsBypassAllChanged?.Invoke();

                Save();
            }
        }

        public event Action OnIsBypassAllChanged;

        public void Load()
        {
            _manager = new Preferences<PreferencesDescriptor>();
            _manager.OnLoaded += Loaded;

            _manager.Load(PreferencePathProvider.Path(_kPreferencesPath));
        }

        void Loaded()
        {
            if (HasAutorun != AutorunProvider.HasSturtup())
                SetAutorun(HasAutorun);
        }

        public void Save()
        {
            _manager.Save();
        }

        void SetAutorun(bool value)
        {
            if (value)
                AutorunProvider.AddToStartup();
            else
                AutorunProvider.RemoveFromStartup();
        }
    }
}
