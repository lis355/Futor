using System.Windows.Forms;

namespace Futor
{
    public partial class OptionsForm : Form
    {
        OptionsFormManager _manager;

        public OptionsForm()
        {
            InitializeComponent();

            _manager = new OptionsFormManager(this);
        }
    }
}
