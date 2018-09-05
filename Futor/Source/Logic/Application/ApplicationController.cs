using System;

namespace Futor
{
    public class ApplicationController
    {
        readonly UITaskbarView _uiTaskbarView;

        public Application Application { get; }

        public event Action OnExit;

        public ApplicationController(Application application)
        {
            Application = application;
            
            _uiTaskbarView = new UITaskbarView(Application);
            _uiTaskbarView.ShowView();

            _uiTaskbarView.Menu.OnBypassAllClicked += () =>
            {
                Application.Options.IsBypassAll.Value = !Application.Options.IsBypassAll.Value;
            };

            _uiTaskbarView.Menu.OnPitchButtonClicked += pitchFactor =>
            {
                Application.Options.PitchFactor.Value = pitchFactor;
            };

            _uiTaskbarView.Menu.OnInputDeviceButtonClicked += inputDeviceName =>
            {
                Application.Options.InputDeviceName.Value = inputDeviceName;
            };

            _uiTaskbarView.Menu.OnOutputDeviceButtonClicked += outputDeviceName =>
            {
                Application.Options.OutputDeviceName.Value = outputDeviceName;
            };

            _uiTaskbarView.Menu.OnExitClicked += () =>
            {
                Exit();
            };

            _uiTaskbarView.ShowView();
        }

        public void Exit()
        {
            Application.Dispose();
            
            _uiTaskbarView.CloseView();
            
            OnExit?.Invoke();
        }
    }
}
