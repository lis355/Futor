using System;

namespace Futor
{
    public class PreferenceController
    {
        Preferences<PreferencesDescriptor> _manager;
        readonly PreferencePathProvider _preferencePathProvider = new PreferencePathProvider();

        public event Action OnLoaded;
        public event Action OnSaved;
        
        public Option<bool> HasAutorun;
        public Option<string> InputDeviceName;
        public Option<string> OutputDeviceName;
        public Option<int> LatencyMilliseconds;
        public Option<int> PitchFactor;
        public Option<bool> IsBypassAll;

        public void Load()
        {
            _manager = new Preferences<PreferencesDescriptor>();
            
            HasAutorun = new Option<bool>(
                () => _manager.Object.HasAutorun,
                value => _manager.Object.HasAutorun = value,
                (sender, args) => Save());

            InputDeviceName = new Option<string>(
                () => _manager.Object.InputDeviceName,
                value => _manager.Object.InputDeviceName = value,
                (sender, args) => Save());

            OutputDeviceName = new Option<string>(
                () => _manager.Object.OutputDeviceName,
                value => _manager.Object.OutputDeviceName = value,
                (sender, args) => Save());

            LatencyMilliseconds = new Option<int>(
                () => _manager.Object.LatencyMilliseconds,
                value => _manager.Object.LatencyMilliseconds = value,
                (sender, args) => Save());

            PitchFactor = new Option<int>(
                () => _manager.Object.PitchFactor,
                value => _manager.Object.PitchFactor = value,
                (sender, args) => Save());

            IsBypassAll = new Option<bool>(
                () => _manager.Object.IsBypassAll,
                value => _manager.Object.IsBypassAll = value,
                (sender, args) => Save());

            _manager.OnLoaded += () =>
            {
                Loaded();
            };
            
            _manager.OnSaved += () =>
            {
                Saved();
            };

            _manager.Load(_preferencePathProvider);
        }

        public void Save()
        {
            _manager.Save();
        }

        void Loaded()
        {
            OnLoaded?.Invoke();
        }

        void Saved()
        {
            OnSaved?.Invoke();
        }
    }
}
