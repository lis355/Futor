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
        }

        public void ShowStack()
        {
            if (StackForm == null)
            {
                StackForm = new StackForm(Stack);

                StackForm.OnSelectPluginButtonClick += (pluginLine) =>
                {

                };
                StackForm.OnMoveUpPluginButtonClick += (pluginLine) =>
                {
                    
                };
                StackForm.OnMoveDownPluginButtonClick += (pluginLine) =>
                {
                    
                };
                StackForm.OnBypassPluginButtonClick += (pluginLine) =>
                {
                    
                };
                StackForm.OnUIPluginButtonClick += (pluginLine) =>
                {
                    ShowPluginUI(pluginLine.Slot);
                };
                StackForm.OnRemovePluginButtonClick += (pluginLine) =>
                {
                    
                };
                StackForm.Closed += (sender, args) =>
                {
                    StackForm = null;

                    _applicationOptions.Save();
                };

                StackForm.Show();
            }

            StackForm.Activate();

            // DEBUG
            var hotkeyForm = new HotkeyForm();
            hotkeyForm.ShowDialog();
            //ShowPluginUI(Stack.PluginSlots.First());
        }

        void ShowPluginUI(PluginsStack.Plugin plugin)
        {
            var pluginUIForm = new PluginUIForm(plugin.PluginContext.PluginCommandStub);
            pluginUIForm.Show();
        }
    }
}
