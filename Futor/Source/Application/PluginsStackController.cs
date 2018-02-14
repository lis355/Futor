namespace Futor
{
    public class PluginsStackController
    {
        readonly ApplicationOptions _applicationOptions;

        public PluginsStack Stack { get; }

        public StackForm StackForm { get; private set; }

        public PluginsStackController(ApplicationOptions applicationOptions, PluginsStack pluginsStack)
        {
            _applicationOptions = applicationOptions;
            Stack = pluginsStack;

            // DEBUG
            //var ttt = PluginsStack.OpenPlugin(@"C:\Program Files\VstPluginsLib\Clip\GClip.dll");
            //var dlg = new PluginUIForm(ttt.PluginCommandStub);
            //dlg.Show();
        }

        public void ShowStack()
        {
            if (StackForm == null)
            {
                StackForm = new StackForm(Stack);

                StackForm.Closed += (sender, args) =>
                {
                    StackForm = null;

                    _applicationOptions.Save();
                };

                StackForm.Show();
            }

            StackForm.Activate();
        }
    }
}
