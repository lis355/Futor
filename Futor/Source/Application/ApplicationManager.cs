using System;
using System.Windows.Forms;

namespace Futor
{
    public class ApplicationManager
    {
        public void Run(Action runAction)
        {
            Preferences<PreferencesDescriptor>.Manager.Load(Application.LocalUserAppDataPath + "\\preferences.xml");

            var pluginsStackProcessor = new PluginsStackProcessor();
            
            var audioManager = new AudioManager
            {
                LatencyMilliseconds = 5,
                SampleProcessor = pluginsStackProcessor 
            };

            pluginsStackProcessor.LoadStack();

            audioManager.Init();
            audioManager.Start();

            var contextMenuProvider = new ContextMenuProvider();

            using (var pi = new ProcessIcon { ContextMenu = contextMenuProvider.ContextMenuStrip })
            {
                pi.Display();

                runAction?.Invoke();
            }
        }
    }
}
