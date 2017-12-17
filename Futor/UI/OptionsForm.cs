using System.Linq;
using System.Windows.Forms;

namespace Futor
{
    public partial class OptionsForm : Form
    {
        readonly AudioManager _audioManager;

        public OptionsForm(AudioManager audioManager)
        {
            InitializeComponent();
            
            _audioManager = audioManager;

            var inputDevices = _audioManager.GetInputMMDevices();
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

                    if (_audioManager.InputDeviceName == deviceName)
                        InputDevicesComboBox.SelectedIndex = InputDevicesComboBox.Items.Count - 1;
                }
            }

            var outputDevices = _audioManager.GetOutputMMDevices();
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

                    if (_audioManager.OutputDeviceName == deviceName)
                        OutputDevicesComboBox.SelectedIndex = OutputDevicesComboBox.Items.Count - 1;
                }
            }
        }

        void OptionsForm_Load(object sender, System.EventArgs e)
        {
        }

        void OptionsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
