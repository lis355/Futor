using System.Linq;
using System.Windows.Forms;

namespace Futor
{
    public partial class OptionsForm : Form
    {
        readonly ApplicationManager _applicationManager;
        readonly bool _isEdit;

        public OptionsForm(ApplicationManager applicationManager)
        {
            InitializeComponent();

            _applicationManager = applicationManager;

            _isEdit = true;

            ProcessAutostart();
            ProcessAudio();

            _isEdit = false;
        }

        void ProcessAutostart()
        {
            AutostartCheckBox.Checked = _applicationManager.HasAutorun;
        }

        void ProcessAudio()
        {
            var audioManager = _applicationManager.AudioManager;

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
            }
        }

        void OptionsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Preferences<PreferencesDescriptor>.Manager.Save();
        }

        void AutostartCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            if (_isEdit)
                return;

            _applicationManager.HasAutorun = AutostartCheckBox.Checked;
        }
    }
}
