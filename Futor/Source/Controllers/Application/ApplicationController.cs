namespace Futor
{
    public class ApplicationController
    {
        readonly TaskbarView _taskbarView;

        public Application Application { get; }

        public ApplicationController(Application application)
        {
            Application = application;
            
            _taskbarView = new TaskbarView(Application);
            _taskbarView.ShowView();

            _taskbarView.Menu.OnBypassAllClicked += () =>
            {
                Application.Options.IsBypassAll.Value = !Application.Options.IsBypassAll.Value;
            };

            _taskbarView.Menu.OnPitchButtonClicked += pitchFactor =>
            {
                Application.Options.PitchFactor.Value = pitchFactor;
            };

            _taskbarView.Menu.OnInputDeviceButtonClicked += inputDeviceName =>
            {
                Application.Options.InputDeviceName.Value = inputDeviceName;
            };

            _taskbarView.Menu.OnOutputDeviceButtonClicked += outputDeviceName =>
            {
                Application.Options.OutputDeviceName.Value = outputDeviceName;
            };

            _taskbarView.Menu.OnExitClicked += () =>
            {
                Exit();
            };

            _taskbarView.ShowView();
        }

        public void Exit()
        {
            _taskbarView.CloseView();
            
            Application.Dispose();
        }
    }
}
