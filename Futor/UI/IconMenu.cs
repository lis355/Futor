using System;
using System.Windows.Forms;

namespace Futor
{
    public partial class IconMenu : UserControl
    {
        bool _isEdit;

        public event Action<bool> OnBypassAllChanged;
        public event Action OnExitClicked;

        public bool BypassAll
        {
            get { return BypassAllStripMenuItem.Checked; }
            set
            {
                _isEdit = true;

                BypassAllStripMenuItem.Checked = value;

                _isEdit = false;
            }
        }

        public IconMenu(Application application)
        {
            InitializeComponent();
            
            BypassAll = application.Options.IsBypassAll;
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
