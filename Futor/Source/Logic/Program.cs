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

            applicationController.OnExit += () =>
            {
                System.Windows.Forms.Application.Exit();
                Environment.Exit(0);
            };

            System.Windows.Forms.Application.Run();
        }
    }
}
