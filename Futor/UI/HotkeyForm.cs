using System;
using System.Windows.Forms;

namespace Futor
{
    public partial class HotkeyForm : Form
    {
        public HotkeyForm()
        {
            InitializeComponent();
        }
        
        private void HotkeyBox_KeyDown(Object sender, KeyEventArgs e)
        {
            var modifierKeys = e.Modifiers;

            var pressedKey = e.KeyData ^ modifierKeys; 
            
            var converter = new KeysConverter();
            HotkeyBox.Text = converter.ConvertToString(e.KeyData);

            HotkeyBox.Text = e.KeyCode.ToString();

            if (e.Shift)
                HotkeyBox.Text += "Shift";
        }
    }
}
