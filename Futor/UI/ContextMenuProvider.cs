using System;
using System.Windows.Forms;

namespace Futor
{
    public partial class ContextMenuProvider : UserControl
    {
        readonly ApplicationManager _applicationManager;
        readonly bool _isEdit;

        public ContextMenuProvider(ApplicationManager applicationManager)
        {
            InitializeComponent();

            _applicationManager = applicationManager;

            _isEdit = true;

            BypassAllStripMenuItem.Checked = Preferences<PreferencesDescriptor>.Instance.IsBypassAll;

            _isEdit = false;
        }

        void ExitStripMenuItem_Click(object sender, EventArgs e)
        {
            _applicationManager.Finish();
        }

        void OptionsStripMenuItem_Click(object sender, EventArgs e)
        {
            _applicationManager.ShowOptions();
        }

        void PluginStripMenuItem_Click(object sender, EventArgs e)
        {
            _applicationManager.ShowStack();
        }

        void BypassAllStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (_isEdit)
                return;

            // TODO как то отображать что включено в PluginLine
            _applicationManager.Stack.IsBypassAll = BypassAllStripMenuItem.Checked;
        }
    }
}
