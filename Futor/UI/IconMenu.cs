using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Futor
{
    public partial class IconMenu : UserControl
    {
        readonly Application _application;
        bool _isEdit;

        public event Action<bool> OnBypassAllChanged;
        public event Action OnExitClicked;
        /*
        public bool BypassAll
        {
            get { return BypassAllStripMenuItem.Checked; }
            set
            {
                _isEdit = true;

                BypassAllStripMenuItem.Checked = value;

                _isEdit = false;
            }
        }*/

        public IconMenu(Application application)
        {
            _application = application;

            InitializeComponent();

            ProcessPitchOptions();

            var applicationOptions = _application.Options;

            BypassAllStripMenuItem.Checked = applicationOptions.HasAutorun;

            //BypassAll = applicationOptions.IsBypassAll;
        }

        void ProcessPitchOptions()
        {
            const int kPitchBorder = 12;

            var pitchValueToolStripMenuItems = new List<ToolStripMenuItem>();

            for (int i = -kPitchBorder; i <= kPitchBorder; i++)
            {
                var pitchValueToolStripMenuItem = new ToolStripMenuItem(string.Format("{0}", i));

                var pitchValue = i;
                pitchValueToolStripMenuItem.Click += (sender, args) =>
                {
                    ChangePitch(pitchValue);
                };

                pitchValueToolStripMenuItems.Add(pitchValueToolStripMenuItem);
            }

            PitchToolStripMenuItem.DropDownItems.AddRange(pitchValueToolStripMenuItems.ToArray());
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

        void ChangePitch(int pitchValue)
        {

        }

        void InputDeviceStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        void OutputDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        void BypassAllStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (_isEdit)
                return;
            
            OnBypassAllChanged?.Invoke(BypassAllStripMenuItem.Checked);
        }

        void ExitStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExitClicked?.Invoke();
        }
    }
}
