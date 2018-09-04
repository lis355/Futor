using System;
using System.Windows.Forms;

namespace Futor
{
    public partial class OptionsForm : Form
    {
        readonly PreferenceController _preferenceController;
        readonly bool _isEdit;

        public event Action OnViewClosed;
        public event Action<bool> OnAutorunChanged;

        public OptionsForm(PreferenceController preferenceController)
        {
            InitializeComponent();

            _preferenceController = preferenceController;

            _isEdit = true;

            ProcessAutostart();

            _isEdit = false;
        }

        void ProcessAutostart()
        {
            AutostartCheckBox.Checked = _preferenceController.HasAutorun.Value;
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
