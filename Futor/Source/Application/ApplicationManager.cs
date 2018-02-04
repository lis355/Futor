using System;
using System.Windows.Forms;

namespace Futor
{
    public class ApplicationManager
    {
        ProcessIcon _processIcon;

        public AudioManager AudioManager { get; private set; }
        
        public void Start()
        {
            Preferences<PreferencesDescriptor>.Manager.Load(DataPathProvider.Path("/preferences.xml"));
            
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

            // DEBUG
            //AudioManager.Start();
            var ttt = PluginsStackProcessor.OpenPlugin(@"C:\Program Files\VstPluginsLib\Clip\GClip.dll");
            var dlg = new PluginUIForm(ttt.PluginCommandStub);
            dlg.Show();

            Preferences<PreferencesDescriptor>.Manager.Save();

            var contextMenuProvider = new ContextMenuProvider(this);

            _processIcon = new ProcessIcon {ContextMenu = contextMenuProvider.ContextRightMenu};
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
