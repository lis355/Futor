using Futor.Source.Application;

namespace Futor
{
    public class ApplicationController
    {
        readonly IView _taskbarView;
        ApplicationOptionsController _optionsController;

        public Application Application { get; }

        public ApplicationController(Application application)
        {
            Application = application;
            
            _taskbarView = new TaskbarView(Application);
            _taskbarView.
            _optionsController = new ApplicationOptionsController(application.Options);
        }

        public void Start()
        {
            Application.Start();

            _taskbarView.ShowView();

            // DEBUG
            ShowStack();
            
            // DEBUG
            //var ttt = PluginsStack.OpenPlugin(@"C:\Program Files\VstPluginsLib\Clip\GClip.dll");
            //var dlg = new PluginUIForm(ttt.PluginCommandStub);
            //dlg.Show();
        }

        public void Finish()
        {
            _taskbarView.CloseView();

            // TODO
            // засунуть в представление с вьюшкой
            HotKeyManager.Instance.Dispose();

            Application.Finish();
        }

        public void ShowStack()
        {
            //if (_stackForm == null)
            //{
            //    _stackForm = new StackForm(this);
            //    _stackForm.Closed += (sender, args) => _stackForm = null;
            //    _stackForm.Show();
            //}
            //
            //_stackForm.Activate();
        }
    }
}
