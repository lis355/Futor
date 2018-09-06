using System;

namespace Futor
{
    public class PreferenceController
    {
        readonly Preferences<PreferencesDescriptor> _manager;
        readonly PreferencePathProvider _preferencePathProvider = new PreferencePathProvider();

        public event Action OnLoaded;
        public event Action OnSaved;
        
        public Option<bool> IsAutorun { get; }
        public Option<string> InputDeviceName { get; }
        public Option<string> OutputDeviceName { get; }
        public Option<int> LatencyMilliseconds { get; }
        public Option<int> PitchFactor { get; }
        public Option<bool> IsBypassAll { get; }

        public PreferenceController()
        {
            _manager = new Preferences<PreferencesDescriptor>();

            IsAutorun = new Option<bool>(
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
        }

        public void Load()
        {
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
