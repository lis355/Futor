using System;

namespace Futor
{
    public class Application
    {
        public ApplicationOptions Options { get; }

        public AudioManager AudioManager { get; private set; }
        public PluginsStack Stack { get; private set; }

        public Application()
        {
            Options = new ApplicationOptions();
        }

        public void Start()
        {
            Options.Load();
            
            ProcessAudioManager();

            // TODO плохая логика
            Options.Save();
        }

        void ProcessAudioManager()
        {
            AudioManager = new AudioManager();

            AudioManager.OnInputDeviceChanged += (sender, args) =>
            {
                Preferences<PreferencesDescriptor>.Instance.InputDeviceName = args.AudioManager.InputDeviceName;
            };

            AudioManager.OnOutputDeviceChanged += (sender, args) =>
            {
                Preferences<PreferencesDescriptor>.Instance.OutputDeviceName = args.AudioManager.OutputDeviceName;
            };

            AudioManager.OnLatencyMillisecondsChanged += (sender, args) =>
            {
                Preferences<PreferencesDescriptor>.Instance.LatencyMilliseconds = args.AudioManager.LatencyMilliseconds;
            };

            AudioManager.InputDeviceName = Preferences<PreferencesDescriptor>.Instance.InputDeviceName;
            AudioManager.OutputDeviceName = Preferences<PreferencesDescriptor>.Instance.OutputDeviceName;
            AudioManager.LatencyMilliseconds = Preferences<PreferencesDescriptor>.Instance.LatencyMilliseconds;

            Stack = new PluginsStack();
            Stack.LoadStack(Preferences<PreferencesDescriptor>.Instance.PluginInfos);

            AudioManager.SampleProcessor = Stack;

            AudioManager.Start();
        }
        
        public void Finish()
        {
            // TODO call dispose

            Options.Save();

            System.Windows.Forms.Application.Exit();
            Environment.Exit(0);
        }
    }
}
