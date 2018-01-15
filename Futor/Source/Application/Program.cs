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

            var applicationManager = new ApplicationManager();
            applicationManager.Start();

            Application.Run();
        }
    }
}
