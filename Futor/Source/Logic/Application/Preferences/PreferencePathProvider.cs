namespace Futor
{
    // %appdata%\..\Local\MBL\Futor\1.0.0.0\

    public class PreferencePathProvider : IPreferencePathProvider
    {
        const string _kPreferencesFileName = "preferences.xml";

        public string Path => System.IO.Path.Combine(System.Windows.Forms.Application.LocalUserAppDataPath, _kPreferencesFileName);
    }
}
