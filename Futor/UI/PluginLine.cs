using System.Windows.Forms;

namespace Futor.UI
{
    public partial class PluginLine : UserControl
    {
        public PluginLine()
        {
            InitializeComponent();
        }

        void LabelPanel_Click(object sender, System.EventArgs e)
        {
            SelectPlugin();
        }

        void PluginNameLabel_Click(object sender, System.EventArgs e)
        {
            SelectPlugin();
        }

        void SelectPlugin()
        {
            MessageBox.Show("1");
        }
    }
}
