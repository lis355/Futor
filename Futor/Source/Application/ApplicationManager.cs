using System;
using System.IO;
using System.Windows.Forms;

namespace Futor
{
    public class ApplicationManager
    {
        ProcessIcon _processIcon;

        public AudioManager AudioManager { get; private set; }

        public ApplicationManager()
        {
        }

        public void Start()
        {
            // %appdata%\..\Local\MBL\Futor\1.0.0.0
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
         
            AudioManager.Start();

            Preferences<PreferencesDescriptor>.Manager.Save();

            var contextMenuProvider = new ContextMenuProvider(this);

            _processIcon = new ProcessIcon {ContextMenu = contextMenuProvider.ContextMenu};
            _processIcon.Display();
        }

        public void Finish()
        {
            _processIcon.Dispose();

            // ... todo call dispose

            Preferences<PreferencesDescriptor>.Manager.Save();

            Application.Exit();
            Environment.Exit(0);
        }
    }
}
