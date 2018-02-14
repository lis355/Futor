using System.Windows.Forms;
using Microsoft.Win32;

namespace Futor
{
    public static class AutorunProvider
    {
        const string _kRegistryRunPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        public static bool HasSturtup()
        {
            return (string)OpenRegistryKey().GetValue(GetApplicationName(), string.Empty) == GetApplicationPath();
        }

        public static void AddToStartup()
        {
            OpenRegistryKey().SetValue(GetApplicationName(), GetApplicationPath());
        }

        public static void RemoveFromStartup()
        {
            OpenRegistryKey().DeleteValue(GetApplicationName(), false);
        }

        static RegistryKey OpenRegistryKey()
        {
            return Registry.CurrentUser.OpenSubKey(_kRegistryRunPath, true);
        }

        static string GetApplicationName()
        {
            return System.Windows.Forms.Application.ProductName;
        }

        static string GetApplicationPath()
        {
            return System.Windows.Forms.Application.ExecutablePath;
        }
    }
}
