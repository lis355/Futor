using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Futor
{
    public partial class IconMenu : UserControl
    {
        readonly Application _application;

        public event Action OnBypassAllClicked;
        public event Action<int> OnPitchButtonClicked;
        public event Action OnExitClicked;

        public IconMenu(Application application)
        {
            _application = application;

            InitializeComponent();

            CreatePitchOptions();

            SetBypassAllStripMenuItemCheckedState();

            _application.Options.OnIsBypassAllChanged += () =>
            {
                SetBypassAllStripMenuItemCheckedState();
            };
        }

        void SetBypassAllStripMenuItemCheckedState()
        {
            BypassAllStripMenuItem.Checked = _application.Options.IsBypassAll;
        }

        void CreatePitchOptions()
        {
            const int kPitchBorder = 12;

            var pitchValueToolStripMenuItems = new List<ToolStripMenuItem>();

            for (int i = -kPitchBorder; i <= kPitchBorder; i++)
            {
                var pitchValueToolStripMenuItem = new ToolStripMenuItem(i.ToString());

                var pitchFactor = i;

                if (_application.Options.PitchFactor == pitchFactor)
                    pitchValueToolStripMenuItem.Checked = true;

                pitchValueToolStripMenuItem.Click += (sender, args) =>
                {
                    OnPitchButtonClicked?.Invoke(pitchFactor);
                };

                pitchValueToolStripMenuItems.Add(pitchValueToolStripMenuItem);
            }

            PitchToolStripMenuItem.DropDownItems.AddRange(pitchValueToolStripMenuItems.Cast<ToolStripItem>().ToArray());
        }
        
        void ProcessAudioDeviceOptions()
        {
            var audioManager = _application.AudioManager;
            /*
            var inputDevices = audioManager.GetInputMMDevices();
            if (!inputDevices.Any())
            {
                InputDevicesComboBox.Items.Add("--");
                InputDevicesComboBox.SelectedIndex = 0;
            }
            else
            {
                foreach (var inputDevice in inputDevices)
                {
                    var deviceName = inputDevice.FriendlyName;
                    InputDevicesComboBox.Items.Add(deviceName);

                    if (audioManager.InputDeviceName == deviceName)
                        InputDevicesComboBox.SelectedIndex = InputDevicesComboBox.Items.Count - 1;
                }
            }

            var outputDevices = audioManager.GetOutputMMDevices();
            if (!outputDevices.Any())
            {
                OutputDevicesComboBox.Items.Add("--");
            }
            else
            {
                foreach (var outputDevice in outputDevices)
                {
                    var deviceName = outputDevice.FriendlyName;
                    OutputDevicesComboBox.Items.Add(deviceName);

                    if (audioManager.OutputDeviceName == deviceName)
                        OutputDevicesComboBox.SelectedIndex = OutputDevicesComboBox.Items.Count - 1;
                }
            }*/
        }

        void BypassAllStripMenuItem_Click(object sender, EventArgs e)
        {
            OnBypassAllClicked?.Invoke();
        }

        void InputDeviceStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        void OutputDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        void ExitStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExitClicked?.Invoke();
        }
    }
}
