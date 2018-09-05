using System;

namespace Futor
{
    public class Application : IDisposable
    {
        PitchShifter _pitchShifter;

        public PreferenceController Options { get; }

        public AudioManager AudioManager { get; private set; }
        
        public Application()
        {
            Options = new PreferenceController();
            Options.OnLoaded += () =>
            {
                OptionsLoaded();
            };

            Options.Load();

            ProcessAudioManager();
        }

        void OptionsLoaded()
        {
            var hasAutorun = Options.HasAutorun.Value;
            if (hasAutorun != AutorunProvider.HasSturtup())
                SetAutorun(hasAutorun);
        }

        void SetAutorun(bool value)
        {
            if (value)
                AutorunProvider.AddToStartup();
            else
                AutorunProvider.RemoveFromStartup();
        }

        void ProcessAudioManager()
        {
            AudioManager = new AudioManager();
            
            AudioManager.OnInputDeviceChanged += (sender, args) =>
            {
                Options.InputDeviceName.Value = args.AudioManager.InputDeviceName;
            };
            
            AudioManager.OnOutputDeviceChanged += (sender, args) =>
            {
                Options.OutputDeviceName.Value = args.AudioManager.OutputDeviceName;
            };
            
            AudioManager.OnLatencyMillisecondsChanged += (sender, args) =>
            {
                Options.LatencyMilliseconds.Value = args.AudioManager.LatencyMilliseconds;
            };

            SetInputDeviceName();
            SetOutputDeviceName();
            SetLatencyMilliseconds();

            _pitchShifter = new PitchShifter();
            SetPitchFactor();

            AudioManager.SampleProcessor = _pitchShifter;

            Options.PitchFactor.OnChanged += (sender, args) => 
            {
                SetPitchFactor();
            };

            Options.InputDeviceName.OnChanged += (sender, args) =>
            {
                SetInputDeviceName();
            };

            Options.OutputDeviceName.OnChanged += (sender, args) =>
            {
                SetOutputDeviceName();
            };

            Options.LatencyMilliseconds.OnChanged += (sender, args) =>
            {
                SetLatencyMilliseconds();
            };

            Options.IsBypassAll.OnChanged += (sender, args) =>
            {
                if (args.NewValue
                    && AudioManager.IsWorking.Value)
                {
                    AudioManager.Finish();
                }
                else if (!args.NewValue
                     && !AudioManager.IsWorking.Value)
                {
                    AudioManager.Start();
                }
            };

            AudioManager.Start();
        }

        void SetInputDeviceName()
        {
            if (!Options.IsBypassAll.Value)
                AudioManager.InputDeviceName = Options.InputDeviceName.Value;
        }

        void SetOutputDeviceName()
        {
            if (!Options.IsBypassAll.Value)
                AudioManager.OutputDeviceName = Options.OutputDeviceName.Value;
        }

        void SetLatencyMilliseconds()
        {
            if (!Options.IsBypassAll.Value)
                AudioManager.LatencyMilliseconds = Options.LatencyMilliseconds.Value;
        }

        void SetPitchFactor()
        {
            _pitchShifter.PitchFactor.Value = Options.PitchFactor.Value;
        }

        public void Dispose()
        {
            Exit();
        }

        public void Exit()
        {
            if (AudioManager.IsWorking.Value)
                AudioManager.Finish();

            Options.Save();
        }
    }
}
