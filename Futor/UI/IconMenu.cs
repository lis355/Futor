using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Futor
{
    public partial class IconMenu : UserControl
    {
        readonly Application _application;
        readonly Dictionary<int, ToolStripMenuItem> _pitchToolStripMenuItems = new Dictionary<int, ToolStripMenuItem>();

        public event Action OnBypassAllClicked;
        public event Action<int> OnPitchButtonClicked;
        public event Action<string> OnInputDeviceButtonClicked;
        public event Action<string> OnOutputDeviceButtonClicked;
        public event Action OnExitClicked;

        public IconMenu(Application application)
        {
            _application = application;

            InitializeComponent();

            CreatePitchOptions();

            SetPitchFactorToolStripMenuItem();
            SetBypassAllStripMenuItemCheckedState();
            
            CreateAudioDeviceOptions();

            SetInputDeviceStripMenuItem();
            SetOutputDeviceStripMenuItem();

            application.Options.OnPitchFactorChanged += () =>
            {
                SetPitchFactorToolStripMenuItem();
            };

            _application.Options.OnIsBypassAllChanged += () =>
            {
                SetBypassAllStripMenuItemCheckedState();
            };

            _application.Options.OnInputDeviceNameChanged += () =>
            {
                SetInputDeviceStripMenuItem();
            };

            _application.Options.OnOutputDeviceNameChanged += () =>
            {
                SetOutputDeviceStripMenuItem();
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

        void SetPitchFactorToolStripMenuItem()
        {
            foreach (var pitchToolStripMenuItem in PitchToolStripMenuItem.DropDownItems.Cast<ToolStripMenuItem>())
                pitchToolStripMenuItem.Checked = (int)pitchToolStripMenuItem.Tag == _application.Options.PitchFactor;
        }

        void SetBypassAllStripMenuItemCheckedState()
        {
            BypassAllStripMenuItem.Checked = _application.Options.IsBypassAll;
        }

        void CreateAudioDeviceOptions()
        {
            var audioManager = _application.AudioManager;

            InputDeviceStripMenuItem.DropDownItems.Add("--");

            foreach (var inputDevice in audioManager.GetInputMMDevices())
                InputDeviceStripMenuItem.DropDownItems.Add(inputDevice.FriendlyName);

            foreach (var inputDeviceItem in InputDeviceStripMenuItem.DropDownItems.Cast<ToolStripMenuItem>())
            {
                inputDeviceItem.Click += (sender, args) =>
                {
                    OnInputDeviceButtonClicked?.Invoke(inputDeviceItem.Text);
                };
            }
            
            OutputDeviceToolStripMenuItem.DropDownItems.Add("--");

            foreach (var outputDevice in audioManager.GetOutputMMDevices())
                OutputDeviceToolStripMenuItem.DropDownItems.Add(outputDevice.FriendlyName);

            foreach (var outputDeviceItem in OutputDeviceToolStripMenuItem.DropDownItems.Cast<ToolStripMenuItem>())
            {
                outputDeviceItem.Click += (sender, args) =>
                {
                    OnOutputDeviceButtonClicked?.Invoke(outputDeviceItem.Text);
                };
            }
        }

        void SetInputDeviceStripMenuItem()
        {
            foreach (var inputDeviceItem in InputDeviceStripMenuItem.DropDownItems.Cast<ToolStripMenuItem>())
                inputDeviceItem.Checked = inputDeviceItem.Text == _application.Options.InputDeviceName;
        }

        void SetOutputDeviceStripMenuItem()
        {
            foreach (var outputDeviceItem in OutputDeviceToolStripMenuItem.DropDownItems.Cast<ToolStripMenuItem>())
                outputDeviceItem.Checked = outputDeviceItem.Text == _application.Options.OutputDeviceName;
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
