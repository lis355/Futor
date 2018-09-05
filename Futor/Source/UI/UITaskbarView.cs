using Futor.Properties;

namespace Futor
{
    public class UITaskbarView
    {
        readonly Application _application;
        readonly UITaskbarIcon _uiTaskbarIcon;
        
        public UIIconMenu Menu { get; }

        public UITaskbarView(Application application)
        {
            _application = application;

            Menu = new UIIconMenu(_application);

            _uiTaskbarIcon = new UITaskbarIcon {ContextMenu = Menu.ContextRightMenu};
            _uiTaskbarIcon.IsShowMenuOnLeftClick = true;

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

            _uiTaskbarIcon.Icon = (active) ? Resources.FiconEnable : Resources.FiconDisable;
        }

        public void ShowView()
        {
            _uiTaskbarIcon.Display();
        }

        public void CloseView()
        {
            _uiTaskbarIcon.Dispose();
        }
    }
}
