using System.Windows.Forms;

namespace Futor
{
    public static class DataPathProvider
    {
        public static string Path(string localPath)
        {
            // %appdata%\..\Local\MBL\Futor\1.0.0.0

            return Application.LocalUserAppDataPath + localPath;
        }
    }
}
