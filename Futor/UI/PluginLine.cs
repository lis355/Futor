using System.Windows.Forms;

namespace Futor.UI
{
    public partial class PluginLine : UserControl
    {
        public PluginLine()
        {
            InitializeComponent();
        }

        private void LabelPanel_Click(object sender, System.EventArgs e)
        {
            SelectPlugin();
        }

        private void PluginNameLabel_Click(object sender, System.EventArgs e)
        {
            SelectPlugin();
        }

        void SelectPlugin()
        {
            MessageBox.Show("1");
        }
    }
}
