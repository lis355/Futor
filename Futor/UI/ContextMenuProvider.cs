using System;
using System.Windows.Forms;

namespace Futor
{
    public partial class ContextMenuProvider : UserControl
    {
        readonly ApplicationManager _applicationManager;

        public ContextMenuProvider(ApplicationManager applicationManager)
        {
            InitializeComponent();

            _applicationManager = applicationManager;
        }

        void ExitStripMenuItem_Click(object sender, EventArgs e)
        {
            _applicationManager.Finish();
        }

        void OptionsStripMenuItem_Click(object sender, EventArgs e)
        {
            _applicationManager.ShowOptions();
        }

        void PluginStripMenuItem_Click(Object sender, EventArgs e)
        {
            _applicationManager.ShowStack();
        }
    }
}
