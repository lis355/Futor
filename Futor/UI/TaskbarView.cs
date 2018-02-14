using System;

namespace Futor
{
    public class TaskbarView : IView
    {
        readonly TaskbarIcon _taskbarIcon;
        readonly IconMenu _iconMenu;
        
        public IMainMenuView MainMenuView { get { return _iconMenu; } }

        public event Action OnViewClosed;

        public TaskbarView(Application application)
        {
            _iconMenu = new IconMenu(application);
            _taskbarIcon = new TaskbarIcon {ContextMenu = _iconMenu.ContextRightMenu};
        }

        public void ShowView()
        {
            _taskbarIcon.Display();
        }

        public void CloseView()
        {
            _taskbarIcon.Dispose();

            OnViewClosed?.Invoke();
        }
    }
}
