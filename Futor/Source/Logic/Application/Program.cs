using System;

namespace Futor
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            var applicationController = new ApplicationController(new Application());

            System.Windows.Forms.Application.Run();
        }
    }
}
