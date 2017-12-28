using System;
using System.Windows.Forms;

namespace Futor
{
    public class ApplicationManager
    {
        public AudioManager AudioManager { get; private set; }

        public void Run(Action runAction)
        {
            Preferences<PreferencesDescriptor>.Manager.Load(Application.LocalUserAppDataPath + "\\preferences.xml");


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
            
            var pluginsStackProcessor = new PluginsStackProcessor();
            pluginsStackProcessor.LoadStack();

            AudioManager.SampleProcessor = pluginsStackProcessor;
         
            AudioManager.Init();
            AudioManager.Start();

            Preferences<PreferencesDescriptor>.Manager.Save();

            var contextMenuProvider = new ContextMenuProvider(this);

            using (var pi = new ProcessIcon { ContextMenu = contextMenuProvider.ContextMenuStrip })
            {
                pi.Display();

                runAction?.Invoke();
            }
        }

        public void Exit()
        {
            // ... todo call dispose

            Application.Exit();
            Environment.Exit(0);
        }
    }
}
