using System;
using Futor.Properties;

namespace Futor
{
    public class TaskbarView
    {
        readonly Application _application;
        readonly TaskbarIcon _taskbarIcon;
        
        public IconMenu Menu { get; }

        public event Action OnLeftMouseClick;

        public TaskbarView(Application application)
        {
            _application = application;

            Menu = new IconMenu(application);

            _taskbarIcon = new TaskbarIcon {ContextMenu = Menu.ContextRightMenu};

            SetTaskBarIcon();

            _taskbarIcon.OnLeftMouseClick += (sender, args) =>
            {
                OnLeftMouseClick?.Invoke();
            };

            application.Options.OnIsBypassAllChanged += () =>
            {
                SetTaskBarIcon();
            };
        }

        void SetTaskBarIcon()
        {
            _taskbarIcon.Icon = (_application.Options.IsBypassAll) ? Resources.FiconEnable : Resources.FiconDisable;
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
