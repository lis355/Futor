using System;

namespace Futor
{
    public class TaskbarView
    {
        readonly TaskbarIcon _taskbarIcon;
        
        public IconMenu Menu { get; }

        public event Action OnLeftMouseClick;

        public TaskbarView(Application application)
        {
            Menu = new IconMenu(application);

            _taskbarIcon = new TaskbarIcon {ContextMenu = Menu.ContextRightMenu};
            _taskbarIcon.OnLeftMouseClick += (sender, args) =>
            {
                OnLeftMouseClick?.Invoke();
            };
        }

        public void ShowView()
        {
            _taskbarIcon.Display();
        }

        public void CloseView()
        {
            _taskbarIcon.Dispose();
        }
    }
}
