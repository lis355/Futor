using System;
using System.Windows.Forms;

namespace Futor
{
    public partial class ContextMenuProvider : UserControl
    {
        public ContextMenuProvider()
        {
            InitializeComponent();
        }

        void ExitStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
