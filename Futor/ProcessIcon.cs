using System;
using System.Windows.Forms;
using Futor.Properties;

namespace Futor
{
    public class ProcessIcon : IDisposable
    {
        readonly NotifyIcon _notifyIcon;

        public MouseEventHandler OnLeftMouseClick { get; set; }
        public Func<string> TextProvider { get; set; }

        public ContextMenuStrip ContextMenu
        {
            get { return _notifyIcon.ContextMenuStrip; }
            set { _notifyIcon.ContextMenuStrip = value; }
        }

        public ProcessIcon()
        {
            _notifyIcon = new NotifyIcon();
        }

        public void Display()
        {
            _notifyIcon.Icon = Resources.MainIcon;
            UpdateText();
            _notifyIcon.Visible = true;

            _notifyIcon.MouseMove += NotifyIconMouseMove;
            _notifyIcon.MouseClick += NotifyIconMouseClick;
        }

        void UpdateText()
        {
            _notifyIcon.Text = Application.ProductName;
        }

        void NotifyIconMouseMove(object sender, MouseEventArgs e)
        {
            UpdateText();
        }
        
        public void Dispose()
        {
            _notifyIcon.Dispose();
        }

        string GetText()
        {
            return (TextProvider != null) ? TextProvider() : Application.ProductName;
        }

        void NotifyIconMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left
                && OnLeftMouseClick != null)
            {
                OnLeftMouseClick(sender, e);
            }
        }
    }
}
