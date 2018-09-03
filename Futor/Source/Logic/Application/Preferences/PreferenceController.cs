using System;

namespace Futor
{
    public class PreferenceController
    {
        Preferences<PreferencesDescriptor> _manager;
        readonly PreferencePathProvider _preferencePathProvider = new PreferencePathProvider();

        public event Action OnLoaded;
        public event Action OnSaved;

        public bool HasAutorun
        {
            get => _manager.Object.HasAutorun;
            set
            {
                if (_manager.Object.HasAutorun == value)
                    return;

                _manager.Object.HasAutorun = value;

                OnHasAutorunChanged?.Invoke();

                Save();
            }
        }

        public event Action OnHasAutorunChanged;

        public string InputDeviceName
        {
            get => _manager.Object.InputDeviceName;
            set
            {
                if (_manager.Object.InputDeviceName == value)
                    return;

                _manager.Object.InputDeviceName = value;

                OnInputDeviceNameChanged?.Invoke();

                Save();
            }
        }

        public event Action OnInputDeviceNameChanged;
    
        public string OutputDeviceName
        {
            get => _manager.Object.OutputDeviceName;
            set
            {
                if (_manager.Object.OutputDeviceName == value)
                    return;

                _manager.Object.OutputDeviceName = value;

                OnOutputDeviceNameChanged?.Invoke();

                Save();
            }
        }

        public event Action OnOutputDeviceNameChanged;

        public int LatencyMilliseconds
        {
            get => _manager.Object.LatencyMilliseconds;
            set
            {
                if (_manager.Object.LatencyMilliseconds == value)
                    return;

                _manager.Object.LatencyMilliseconds = value;

                OnLatencyMillisecondsChanged?.Invoke();

                Save();
            }
        }

        public event Action OnLatencyMillisecondsChanged;

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

            _manager.OnLoaded += () =>
            {
                OnLoaded?.Invoke();
            };
            
            _manager.OnSaved += () =>
            {
                OnSaved?.Invoke();
            };

            _manager.Load(_preferencePathProvider.Path);
        }

        public void Save()
        {
            _manager.Save();
        }
    }
}
