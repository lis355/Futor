﻿using System.Windows.Forms;

namespace Futor
{
    public static class DataPathProvider
    {
        public static string Path(string localPath)
        {
            // %appdata%\..\Local\MBL\Futor\

            return Application.LocalUserAppDataPath + localPath;
        }
    }
}