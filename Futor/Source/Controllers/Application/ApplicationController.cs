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
                Application.Options.IsBypassAll = !Application.Options.IsBypassAll;
            };
            _taskbarView.Menu.OnPitchButtonClicked += pitchFactor =>
            {
                Application.Options.PitchFactor = pitchFactor;
            };
            _taskbarView.Menu.OnInputDeviceButtonClicked += inputDeviceName =>
            {
                Application.Options.InputDeviceName = inputDeviceName;
            };
            _taskbarView.Menu.OnOutputDeviceButtonClicked += outputDeviceName =>
            {
                Application.Options.OutputDeviceName = outputDeviceName;
            };
            _taskbarView.Menu.OnExitClicked += Exit;

            _taskbarView.ShowView();
        }

        public void Exit()
        {
            _taskbarView.CloseView();
            
            Application.Dispose();
        }
    }
}
