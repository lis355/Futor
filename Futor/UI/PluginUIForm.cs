using System;
using System.Drawing;
using System.Windows.Forms;
using Jacobi.Vst.Core.Host;

namespace Futor
{
    public partial class PluginUIForm : Form
    {
        readonly IVstPluginCommandStub _pluginCommandStub;

        public PluginUIForm(IVstPluginCommandStub pluginCommandStub)
        {
            InitializeComponent();

            _pluginCommandStub = pluginCommandStub;
        }

        public new void Show()
        {
            Rectangle wndRect;

            Text = _pluginCommandStub.GetEffectName();

            if (_pluginCommandStub.EditorGetRect(out wndRect))
            {
                ViewPanel.Size = new Size(wndRect.Width, wndRect.Height);
                var maxW = Math.Max(ViewPanel.Width, PresetsPanel.Width);
                ViewPanel.Location = new Point((maxW - ViewPanel.Width) / 2, 0);
                PresetsPanel.Location = new Point((maxW - PresetsPanel.Width) / 2, ViewPanel.Height);

                Size = SizeFromClientSize(new Size(maxW, ViewPanel.Height + PresetsPanel.Height));

                _pluginCommandStub.EditorOpen(ViewPanel.Handle);
            }
            else
            {
                throw new Exception("Plugin hasn't UI.");
            }

            base.Show();
        }
        
        void EditorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _pluginCommandStub.EditorClose();
        }
    }
}
