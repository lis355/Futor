using System.Windows.Forms;

namespace Futor
{
    public partial class OptionsForm : Form
    {
        readonly OptionsFormManager _manager;

        public OptionsForm()
        {
            InitializeComponent();

            _manager = new OptionsFormManager(this);
        }

        void OptionsForm_Load(object sender, System.EventArgs e)
        {
            _manager.OnFormLoad();
        }

        void OptionsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _manager.OnFormClosed();
        }
    }
}
