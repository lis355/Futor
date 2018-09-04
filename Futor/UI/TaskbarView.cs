using Futor.Properties;

namespace Futor
{
    public class TaskbarView
    {
        readonly Application _application;
        readonly TaskbarIcon _taskbarIcon;
        
        public IconMenu Menu { get; }

        public TaskbarView(Application application)
        {
            _application = application;

            Menu = new IconMenu(_application);

            _taskbarIcon = new TaskbarIcon {ContextMenu = Menu.ContextRightMenu};
            _taskbarIcon.IsShowMenuOnLeftClick = true;

            SetTaskBarIcon();

            _application.Options.PitchFactor.OnChanged += (sender, args) =>
            {
                SetTaskBarIcon();
            };

            _application.Options.IsBypassAll.OnChanged += (sender, args) =>
            {
                SetTaskBarIcon();
            };

            _application.AudioManager.IsWorking.OnChanged += (sender, args) =>
            {
                SetTaskBarIcon();
            };
        }

        void SetTaskBarIcon()
        {
            var active = _application.AudioManager.IsWorking.Value
                && !_application.Options.IsBypassAll.Value
                && _application.Options.PitchFactor.Value != 0;

            _taskbarIcon.Icon = (active) ? Resources.FiconEnable : Resources.FiconDisable;
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
