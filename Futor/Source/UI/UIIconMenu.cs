using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Futor
{
    public partial class UIIconMenu : UserControl
    {
        const string _kEmptyDeviceName = "--";

        readonly Application _application;
        readonly Dictionary<int, ToolStripMenuItem> _pitchToolStripMenuItems = new Dictionary<int, ToolStripMenuItem>();

        public event Action OnBypassAllClicked;
        public event Action<int> OnPitchButtonClicked;
        public event Action<string> OnInputDeviceButtonClicked;
        public event Action<string> OnOutputDeviceButtonClicked;
        public event Action OnExitClicked;

        public UIIconMenu(Application application)
        {
            _application = application;

            InitializeComponent();

            CreatePitchOptions();

            SetPitchFactor();
            SetBypassAll();
            
            CreateAudioDeviceOptions();

            SetInputDeviceName();
            SetOutputDeviceName();

            application.Options.PitchFactor.OnChanged += (sender, args) =>
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
        }

        void CreatePitchOptions()
        {
            const int kPitchBorder = 12;

            for (int i = -kPitchBorder; i <= kPitchBorder; i++)
            {
                var pitchValueToolStripMenuItem = new ToolStripMenuItem(i.ToString());
                pitchValueToolStripMenuItem.Tag = i;

                var pitchFactor = i;
                pitchValueToolStripMenuItem.Click += (sender, args) =>
                {
                    OnPitchButtonClicked?.Invoke(pitchFactor);
                };

                _pitchToolStripMenuItems.Add(pitchFactor, pitchValueToolStripMenuItem);
            }

            PitchToolStripMenuItem.DropDownItems.AddRange(_pitchToolStripMenuItems.Values.Cast<ToolStripItem>().ToArray());
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

        void ExitStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExitClicked?.Invoke();
        }
    }
}
