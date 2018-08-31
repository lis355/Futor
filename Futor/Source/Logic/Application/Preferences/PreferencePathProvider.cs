namespace Futor
{
    // %appdata%\..\Local\MBL\Futor\

    public class PreferencePathProvider : IPreferencePathProvider
    {
        const string _kPreferencesFileName = "preferences.xml";

        public string Path => System.IO.Path.Combine(System.Windows.Forms.Application.LocalUserAppDataPath, _kPreferencesFileName);
    }
}
