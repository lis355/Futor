using System;

namespace Futor
{
    public class ApplicationManager
    {
        public void Run(Action runAction)
        {
            var audioManager = new AudioManager
            {
                LatencyMilliseconds = 5,
                SampleProcessor = new PluginsStackProcessor()
            };

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
