using System;
using System.Windows.Forms;

namespace Futor
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var audioManager = new AudioManager();
            var contextMenuProvider = new ContextMenuProvider();

            using (var pi = new ProcessIcon { ContextMenu = contextMenuProvider.ContextMenuStrip})
            {
                pi.Display();

                Application.Run();
            }
        }
    }
}
