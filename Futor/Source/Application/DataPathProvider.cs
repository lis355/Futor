namespace Futor
{
    public static class DataPathProvider
    {
        public static string Path(string localPath)
        {
            // %appdata%\..\Local\MBL\Futor\

            return System.Windows.Forms.Application.LocalUserAppDataPath + localPath;
        }
    }
}
