using System;
using System.Windows.Forms;

namespace Futor
{
    public class ApplicationManager
    {
        public void Run(Action runAction)
        {
            Preferences<PreferencesDescriptor>.Manager.Load(Application.LocalUserAppDataPath + "\\preferences.xml");

            var pluginsStackProcessor = new PluginsStackProcessor();

            var audioManager = new AudioManager();

            audioManager.OnInputDeviceChanged += (sender, args) =>
            {
                Preferences<PreferencesDescriptor>.Instance.InputDeviceName = args.AudioManager.InputDeviceName;
            };

            audioManager.OnOutputDeviceChanged += (sender, args) =>
            {
                Preferences<PreferencesDescriptor>.Instance.OutputDeviceName = args.AudioManager.OutputDeviceName;
            };

            audioManager.OnLatencyMillisecondsChanged += (sender, args) =>
            {
                Preferences<PreferencesDescriptor>.Instance.LatencyMilliseconds = args.AudioManager.LatencyMilliseconds;
            };

            audioManager.InputDeviceName = Preferences<PreferencesDescriptor>.Instance.InputDeviceName;
            audioManager.OutputDeviceName = Preferences<PreferencesDescriptor>.Instance.OutputDeviceName;
            audioManager.LatencyMilliseconds = Preferences<PreferencesDescriptor>.Instance.LatencyMilliseconds;
            audioManager.SampleProcessor = pluginsStackProcessor;
         
            pluginsStackProcessor.LoadStack();

            audioManager.Init();
            audioManager.Start();

            Preferences<PreferencesDescriptor>.Manager.Save();

            var contextMenuProvider = new ContextMenuProvider();

            using (var pi = new ProcessIcon { ContextMenu = contextMenuProvider.ContextMenuStrip })
            {
                pi.Display();

                runAction?.Invoke();
            }
        }
    }
}
