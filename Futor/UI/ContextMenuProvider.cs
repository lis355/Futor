using System;
using System.IO;
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
            Environment.Exit(0);
        }

        void AddPluginStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

            if (Directory.Exists(Preferences<PreferencesDescriptor>.Instance.LastPluginPath))
                openFileDialog.InitialDirectory = Preferences<PreferencesDescriptor>.Instance.LastPluginPath;

            openFileDialog.Filter = "VST Plugins|*.dll";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // ...

                Preferences<PreferencesDescriptor>.Instance.LastPluginPath = Path.GetDirectoryName(openFileDialog.FileName);
                Preferences<PreferencesDescriptor>.Manager.Save();
            }
        }
    }
}
