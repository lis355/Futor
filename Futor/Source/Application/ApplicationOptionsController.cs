namespace Futor.Source.Application
{
    class ApplicationOptionsController
    {
        IOptionsView _optionsView;

        public ApplicationOptions Options { get; }

        public ApplicationOptionsController(ApplicationOptions applicationOptions)
        {
            Options = applicationOptions;
            _optionsView = new OptionsForm(applicationOptions);
            _optionsView.OnAutorunChanged += OptionsViewOnAutorunChanged;
        }

        void OptionsViewOnAutorunChanged(bool value)
        {
            
        }

        public void ShowOptions()
        {
            if (_optionsForm == null)
            {
                _optionsForm = new OptionsForm(Options);
                _optionsForm.OnViewClosed += OptionsFormOnViewClosed;
                _optionsForm.OnAutorunChanged += OptionsFormOnAutorunChanged;
            
                _optionsForm.Closed += (sender, args) => _optionsForm = null;
                _optionsForm.Show();
            }
            
            _optionsForm.Activate();
        }

        void OptionsFormOnViewClosed()
        {
            Options.Save();
        }

        void OptionsFormOnAutorunChanged(bool value)
        {
            Options.HasAutorun = value;
        }
    }
}
