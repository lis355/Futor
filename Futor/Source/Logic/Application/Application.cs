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
            var hasAutorun = Options.HasAutorun;
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
            AudioManager = new AudioManager
            {
                InputDeviceName = Options.InputDeviceName,
                OutputDeviceName = Options.OutputDeviceName,
                LatencyMilliseconds = Options.LatencyMilliseconds
            };

            AudioManager.OnInputDeviceChanged += (sender, args) =>
            {
                Options.InputDeviceName = args.AudioManager.InputDeviceName;
            };
            
            AudioManager.OnOutputDeviceChanged += (sender, args) =>
            {
                Options.OutputDeviceName = args.AudioManager.OutputDeviceName;
            };
            
            AudioManager.OnLatencyMillisecondsChanged += (sender, args) =>
            {
                Options.LatencyMilliseconds = args.AudioManager.LatencyMilliseconds;
            };
            
            _pitchShifter = new PitchShifter();
            SetPitchFactor();

            AudioManager.SampleProcessor = _pitchShifter;

            Options.OnPitchFactorChanged += () =>
            {
                _pitchShifter.PitchFactor = Options.PitchFactor;
            };

            Options.OnInputDeviceNameChanged += () =>
            {
                AudioManager.InputDeviceName = Options.InputDeviceName;
            };

            Options.OnOutputDeviceNameChanged += () =>
            {
                AudioManager.OutputDeviceName = Options.OutputDeviceName;
            };

            Options.OnLatencyMillisecondsChanged += () =>
            {
                AudioManager.LatencyMilliseconds = Options.LatencyMilliseconds;
            };

            AudioManager.Start();
        }

        void SetPitchFactor()
        {
            _pitchShifter.PitchFactor = Options.PitchFactor;
        }

        public void Dispose()
        {
            Exit();
        }

        public void Exit()
        {
            // TODO call dispose

            Options.Save();

            System.Windows.Forms.Application.Exit();
            Environment.Exit(0);
        }
    }
}
