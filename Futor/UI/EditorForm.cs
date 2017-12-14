using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Jacobi.Vst.Core.Host;

namespace Futor
{
    public partial class EditorForm : Form
    {
        public EditorForm()
        {
            InitializeComponent();
        }
        
        public IVstPluginCommandStub PluginCommandStub { get; set; }
        
        public new void ShowDialog()
        {
            Rectangle wndRect;

            Text = PluginCommandStub.GetEffectName();

            if (PluginCommandStub.EditorGetRect(out wndRect))
            {
                Size = SizeFromClientSize(new Size(wndRect.Width, wndRect.Height));

                PluginCommandStub.EditorOpen(Handle);
            }

            base.ShowDialog();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (e.Cancel == false)
                PluginCommandStub.EditorClose();
        }
    }
}
