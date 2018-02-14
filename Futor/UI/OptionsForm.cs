using System;
using System.Windows.Forms;

namespace Futor
{
    public partial class OptionsForm : Form, IOptionsView
    {
        readonly ApplicationOptions _applicationOptions;
        readonly bool _isEdit;

        public event Action OnViewClosed;
        public event Action<bool> OnAutorunChanged;

        public OptionsForm(ApplicationOptions applicationOptions)
        {
            InitializeComponent();

            _applicationOptions = applicationOptions;

            _isEdit = true;

            ProcessAutostart();
            ProcessAudio();

            _isEdit = false;
        }

        void ProcessAutostart()
        {
            AutostartCheckBox.Checked = _applicationOptions.HasAutorun;
        }

        void ProcessAudio()
        {/*
            var audioManager = _application.AudioManager;

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

        void OptionsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            OnViewClosed?.Invoke();
        }

        void AutostartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_isEdit)
                return;

            OnAutorunChanged?.Invoke(AutostartCheckBox.Checked);
        }
    }
}
