using System;
using System.IO;
using System.Windows.Forms;
using Futor.UI;

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

        void ShowStack()
        {
            var stackForm = new StackForm();
            stackForm.ShowDialog();
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
            }
        }

        void OptionsStripMenuItem_Click(object sender, EventArgs e)
        {
            var optionsForm = new OptionsForm(_applicationManager.AudioManager);
            optionsForm.ShowDialog();
        }

        private void EditStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowStack();
        }
    }
}
