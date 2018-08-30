using System;

namespace Futor
{
    public class Application : IDisposable
    {
        public PreferenceController Options { get; }

        public AudioManager AudioManager { get; private set; }

        public Application()
        {
            Options = new PreferenceController();
            Options.Load();

            ProcessAudioManager();
        }

        void ProcessAudioManager()
        {
            AudioManager = new AudioManager();

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

            AudioManager.InputDeviceName = Options.InputDeviceName;
            AudioManager.OutputDeviceName = Options.OutputDeviceName;
            AudioManager.LatencyMilliseconds = Options.LatencyMilliseconds;

            var pitchShifter = new PitchShifter();
            pitchShifter.PitchFactor = Options.PitchFactor;
            AudioManager.SampleProcessor = pitchShifter;
             
            AudioManager.Start();
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
