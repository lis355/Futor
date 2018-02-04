using System;
using System.Windows.Forms;

namespace Futor
{
    public class ApplicationManager
    {
        ContextMenuProvider _contextMenuProvider;
        ProcessIcon _processIcon;
        OptionsForm _optionsForm;
        StackForm _stackForm;

        public AudioManager AudioManager { get; private set; }

        public bool HasAutorun
        {
            get
            {
                return Preferences<PreferencesDescriptor>.Instance.HasAutorun;
            }
            set
            {
                Preferences<PreferencesDescriptor>.Instance.HasAutorun = value;

                if (value)
                    AutorunProvider.AddToStartup();
                else
                    AutorunProvider.RemoveFromStartup();
            }
        }

        public void Start()
        {
            Preferences<PreferencesDescriptor>.Manager.Load(DataPathProvider.Path("/preferences.xml"));

            ProcessAutorun();
            ProcessAudioManager();
            ProcessProcessIcon();

            Preferences<PreferencesDescriptor>.Manager.Save();

            // DEBUG
            ShowStack();
        }

        void ProcessAutorun()
        {
            var preferencesHasAutorun = Preferences<PreferencesDescriptor>.Instance.HasAutorun;
            if (preferencesHasAutorun != AutorunProvider.HasSturtup())
                HasAutorun = preferencesHasAutorun;
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

            var pluginsStackProcessor = new PluginsStackProcessor();
            pluginsStackProcessor.LoadStack();

            AudioManager.SampleProcessor = pluginsStackProcessor;

            AudioManager.Start();

            // DEBUG
            //var ttt = PluginsStackProcessor.OpenPlugin(@"C:\Program Files\VstPluginsLib\Clip\GClip.dll");
            //var dlg = new PluginUIForm(ttt.PluginCommandStub);
            //dlg.Show();
        }

        void ProcessProcessIcon()
        {
            _contextMenuProvider = new ContextMenuProvider(this);

            _processIcon = new ProcessIcon {ContextMenu = _contextMenuProvider.ContextRightMenu};
            _processIcon.Display();
        }

        public void Finish()
        {
            _processIcon.Dispose();

            HotKeyManager.Instance.Dispose();

            // TODO call dispose

            Preferences<PreferencesDescriptor>.Manager.Save();

            Application.Exit();
            Environment.Exit(0);
        }
        
        public void ShowOptions()
        {
            if (_optionsForm == null)
            {
                _optionsForm = new OptionsForm(this);
                _optionsForm.Closed += (sender, args) => _optionsForm = null;
                _optionsForm.Show();
            }

            _optionsForm.Activate();
        }

        public void ShowStack()
        {
            if (_stackForm == null)
            {
                _stackForm = new StackForm();
                _stackForm.Closed += (sender, args) => _stackForm = null;
                _stackForm.Show();
            }

            _stackForm.Activate();
        }
    }
}
