using System;
using System.Windows.Forms;

namespace Futor
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            //readonly AudioManager _audioManager;
            var contextMenuProvider = new ContextMenuProvider();

            using (var pi = new ProcessIcon { ContextMenu = contextMenuProvider.ContextMenuStrip})
            {
                pi.Display();

                Application.Run();
            }
        }
    }
}
