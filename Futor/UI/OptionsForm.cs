using System;
using System.Windows.Forms;

namespace Futor
{
    public partial class OptionsForm : Form
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

            _isEdit = false;
        }

        void ProcessAutostart()
        {
            AutostartCheckBox.Checked = _applicationOptions.HasAutorun;
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
