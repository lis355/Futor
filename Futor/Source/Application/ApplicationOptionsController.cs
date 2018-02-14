namespace Futor
{
    public class ApplicationOptionsController
    {
        public ApplicationOptions Options { get; }

        public OptionsForm OptionsForm { get; private set; }

        public ApplicationOptionsController(ApplicationOptions applicationOptions)
        {
            Options = applicationOptions;
        }

        public void ShowOptions()
        {
            if (OptionsForm == null)
            {
                OptionsForm = new OptionsForm(Options);

                OptionsForm.OnViewClosed += () =>
                {
                    Options.Save();
                };
                OptionsForm.OnAutorunChanged += (value) =>
                {
                    Options.HasAutorun = value;
                };
                OptionsForm.Closed += (sender, args) =>
                {
                    OptionsForm = null;

                    Options.Save();
                };

                OptionsForm.Show();
            }

            OptionsForm.Activate();
        }
    }
}
