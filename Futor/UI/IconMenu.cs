using System;
using System.Windows.Forms;

namespace Futor
{
    public partial class IconMenu : UserControl, IMainMenuView
    {
        readonly bool _isEdit;

        public event Action OnViewClosed;
        public event Action OnShowOptionsClicked;
        public event Action OnShowStackClicked;
        public event Action<bool> OnBypassAllChanged;

        public IconMenu(Application application)
        {
            InitializeComponent();
            

            _isEdit = true;

            BypassAllStripMenuItem.Checked = application.Options.HasAutorun;

            _isEdit = false;
        }
        
        public void ShowView()
        {
            throw new NotImplementedException();
        }

        public void CloseView()
        {
            throw new NotImplementedException();
        }

        void ExitStripMenuItem_Click(object sender, EventArgs e)
        {
            OnViewClosed?.Invoke();
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

            // TODO как то отображать что включено в PluginLine
            OnBypassAllChanged?.Invoke(BypassAllStripMenuItem.Checked);
        }
    }
}
