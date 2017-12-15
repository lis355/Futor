using System;

namespace Futor
{
    public class ApplicationManager
    {
        public void Run(Action runAction)
        {
            // C:\Users\LIS\AppData\Local\Microsoft\Futor\1.0.0.0

            var t = PreferencesManager<PreferencesDescriptor>.Instance.LastPluginPath;

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
