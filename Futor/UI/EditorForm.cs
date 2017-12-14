using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Jacobi.Vst.Core.Host;

namespace Futor
{
    public partial class EditorForm : Form
    {
        readonly IVstPluginCommandStub _pluginCommandStub;

        public EditorForm(IVstPluginCommandStub pluginCommandStub)
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
                Size = SizeFromClientSize(new Size(wndRect.Width, wndRect.Height));

                _pluginCommandStub.EditorOpen(Handle);
            }

            base.Show();
        }
        
        void EditorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _pluginCommandStub.EditorClose();
        }
    }
}
