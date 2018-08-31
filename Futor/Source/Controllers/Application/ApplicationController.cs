﻿namespace Futor
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

            _taskbarView.OnLeftMouseClick += () =>
            {
            };
            _taskbarView.Menu.OnBypassAllClicked += () =>
            {
                Application.Options.IsBypassAll = !Application.Options.IsBypassAll;
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
