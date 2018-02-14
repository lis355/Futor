using System;
using System.Windows.Forms;

namespace Futor
{
    public partial class IconMenu : UserControl
    {
        bool _isEdit;
        
        public event Action OnShowOptionsClicked;
        public event Action OnShowStackClicked;
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
        
        void OptionsStripMenuItem_Click(object sender, EventArgs e)
        {
            OnShowOptionsClicked?.Invoke();
        }

        void PluginStripMenuItem_Click(object sender, EventArgs e)
        {
            OnShowStackClicked?.Invoke();
        }

        void BypassAllStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (_isEdit)
                return;
            
            OnBypassAllChanged?.Invoke(BypassAllStripMenuItem.Checked);
        }

        private void ExitStripMenuItem_Click(Object sender, EventArgs e)
        {
            OnExitClicked?.Invoke();
        }
    }
}
