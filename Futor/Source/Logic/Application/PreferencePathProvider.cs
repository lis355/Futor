namespace Futor
{
    public static class PreferencePathProvider
    {
        public static string Path(string localPath)
        {
            // %appdata%\..\Local\MBL\Futor\

            return System.IO.Path.Combine(System.Windows.Forms.Application.LocalUserAppDataPath, localPath);
        }
    }
}
