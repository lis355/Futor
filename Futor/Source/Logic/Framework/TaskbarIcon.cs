using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Futor
{
    public class TaskbarIcon : IDisposable
    {
        readonly NotifyIcon _notifyIcon;

        public MouseEventHandler OnLeftMouseClick { get; set; }
        public Func<string> TextProvider { get; set; }

        public Icon Icon
        {
            get => _notifyIcon.Icon;
            set => _notifyIcon.Icon = value;
        }

        public ContextMenuStrip ContextMenu
        {
            get => _notifyIcon.ContextMenuStrip;
            set => _notifyIcon.ContextMenuStrip = value;
        }

        public bool IsShowMenuOnLeftClick { get; set; }

        public TaskbarIcon()
        {
            _notifyIcon = new NotifyIcon();
        }

        public void Display()
        {
            UpdateText();
            _notifyIcon.Visible = true;

            _notifyIcon.MouseMove += NotifyIconMouseMove;
            _notifyIcon.MouseClick += NotifyIconMouseClick;
        }

        void UpdateText()
        {
            _notifyIcon.Text = GetText();
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
            return (TextProvider != null) ? TextProvider() : System.Windows.Forms.Application.ProductName;
        }

        void NotifyIconMouseClick(object sender, MouseEventArgs e)
        {
            if (IsShowMenuOnLeftClick)
            {
                var methodInfo = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                methodInfo.Invoke(_notifyIcon, null);
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                    OnLeftMouseClick?.Invoke(sender, e);
            }
        }
    }
}
