using System;
using System.Linq;
using System.Windows.Forms;

namespace Futor
{
    public partial class UIIconMenu : UserControl
    {
        const string _kEmptyDeviceName = "--";

        readonly Application _application;

        public event Action OnBypassAllClicked;
        public event Action<int> OnPitchButtonClicked;
        public event Action<string> OnInputDeviceButtonClicked;
        public event Action<string> OnOutputDeviceButtonClicked;
        public event Action OnAutorunClicked;
        public event Action OnExitClicked;

        public UIIconMenu(Application application)
        {
            _application = application;

            InitializeComponent();

            CreatePitchOptions();

            SetPitchFactor();
            SetBypassAll();
            SetAutorun();

            CreateAudioDeviceOptions();

            SetInputDeviceName();
            SetOutputDeviceName();

            _application.Options.PitchFactor.OnChanged += (sender, args) =>
            {
                SetPitchFactor();
            };

            _application.Options.IsBypassAll.OnChanged += (sender, args) =>
            {
                SetBypassAll();
            };

            _application.Options.InputDeviceName.OnChanged += (sender, args) =>
            {
                SetInputDeviceName();
            };

            _application.Options.OutputDeviceName.OnChanged += (sender, args) =>
            {
                SetOutputDeviceName();
            };

            _application.Options.IsAutorun.OnChanged += (sender, args) =>
            {
                SetAutorun();
            };
        }

        void CreatePitchOptions()
        {
            for (int i = _application.PitchShifter.PitchFactor.Min; i <= _application.PitchShifter.PitchFactor.Max; i++)
            {
                var pitchValueToolStripMenuItem = new ToolStripMenuItem(i.ToString());
                pitchValueToolStripMenuItem.Tag = i;

                var pitchFactor = i;
                pitchValueToolStripMenuItem.Click += (sender, args) => { OnPitchButtonClicked?.Invoke(pitchFactor); };

                PitchToolStripMenuItem.DropDownItems.Add(pitchValueToolStripMenuItem);
            }
        }

        void SetPitchFactor()
        {
            foreach (var pitchToolStripMenuItem in PitchToolStripMenuItem.DropDownItems.Cast<ToolStripMenuItem>())
                pitchToolStripMenuItem.Checked = (int)pitchToolStripMenuItem.Tag == _application.Options.PitchFactor.Value;
        }

        void SetBypassAll()
        {
            BypassAllStripMenuItem.Checked = _application.Options.IsBypassAll.Value;
        }

        void SetAutorun()
        {
            AutorunMenuItem.Checked = _application.Options.IsAutorun.Value;
        }

        void CreateAudioDeviceOptions()
        {
            var audioManager = _application.AudioManager;

            InputDeviceStripMenuItem.DropDownItems.Add(_kEmptyDeviceName);

            foreach (var inputDeviceName in audioManager.GetInputDevicesNames())
                InputDeviceStripMenuItem.DropDownItems.Add(inputDeviceName);

            foreach (var inputDeviceItem in InputDeviceStripMenuItem.DropDownItems.Cast<ToolStripMenuItem>())
            {
                inputDeviceItem.Click += (sender, args) =>
                {
                    var text = inputDeviceItem.Text;
                    if (text == _kEmptyDeviceName)
                        text = string.Empty;

                    OnInputDeviceButtonClicked?.Invoke(text);
                };
            }
            
            OutputDeviceToolStripMenuItem.DropDownItems.Add(_kEmptyDeviceName);

            foreach (var outputDeviceName in audioManager.GetOutputDevicesNames())
                OutputDeviceToolStripMenuItem.DropDownItems.Add(outputDeviceName);

            foreach (var outputDeviceItem in OutputDeviceToolStripMenuItem.DropDownItems.Cast<ToolStripMenuItem>())
            {
                outputDeviceItem.Click += (sender, args) =>
                {
                    var text = outputDeviceItem.Text;
                    if (text == _kEmptyDeviceName)
                        text = string.Empty;

                    OnOutputDeviceButtonClicked?.Invoke(text);
                };
            }
        }

        void SetInputDeviceName()
        {
            var text = _application.Options.InputDeviceName.Value;
            if (string.IsNullOrEmpty(text))
                text = _kEmptyDeviceName;

            foreach (var inputDeviceItem in InputDeviceStripMenuItem.DropDownItems.Cast<ToolStripMenuItem>())
                inputDeviceItem.Checked = inputDeviceItem.Text == text;
        }

        void SetOutputDeviceName()
        {
            var text = _application.Options.OutputDeviceName.Value;
            if (string.IsNullOrEmpty(text))
                text = _kEmptyDeviceName;

            foreach (var outputDeviceItem in OutputDeviceToolStripMenuItem.DropDownItems.Cast<ToolStripMenuItem>())
                outputDeviceItem.Checked = outputDeviceItem.Text == text;
        }

        void BypassAllStripMenuItem_Click(object sender, EventArgs e)
        {
            OnBypassAllClicked?.Invoke();
        }
        
        void AutorunMenuItem_Click(object sender, EventArgs e)
        {
            OnAutorunClicked?.Invoke();
        }

        void ExitStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExitClicked?.Invoke();
        }
    }
}
