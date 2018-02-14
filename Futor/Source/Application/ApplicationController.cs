namespace Futor
{
    public class ApplicationController
    {
        readonly TaskbarView _taskbarView;
        readonly ApplicationOptionsController _optionsController;
        readonly PluginsStackController _pluginsStackController;

        public Application Application { get; }

        public ApplicationController(Application application)
        {
            Application = application;

            _optionsController = new ApplicationOptionsController(Application.Options);
            _pluginsStackController = new PluginsStackController(Application.Options, Application.Stack);

            _taskbarView = new TaskbarView(Application);
            _taskbarView.ShowView();

            _taskbarView.OnLeftMouseClick += () =>
            {
                if (_optionsController.OptionsForm != null)
                    ShowOptions();

                if (_pluginsStackController.StackForm != null)
                    ShowStack();
            };
            _taskbarView.Menu.OnShowOptionsClicked += () =>
            {
                ShowOptions();
            };
            _taskbarView.Menu.OnShowStackClicked += () =>
            {
                ShowStack();
            };
            _taskbarView.Menu.OnBypassAllChanged += (value) =>
            {
                // TODO как то отображать что включено в PluginLine
                Application.Options.IsBypassAll = value;
            };
            _taskbarView.Menu.OnExitClicked += () =>
            {
                Exit();
            };

            _taskbarView.ShowView();

            // DEBUG
            ShowStack();
        }

        void ShowStack()
        {
            _pluginsStackController.ShowStack();
        }

        void ShowOptions()
        {
            _optionsController.ShowOptions();
        }

        public void Exit()
        {
            _taskbarView.CloseView();
            
            HotKeyManager.Instance.Dispose();

            Application.Dispose();
        }
    }
}
