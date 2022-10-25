using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace FontInstaller
{
    [Serializable]
    class FontAlreadyExistsException : Exception
    {
        public FontAlreadyExistsException() { }

        public FontAlreadyExistsException(string fontName)
            : base(String.Format("Font already exists: {0}", fontName))
        {

        }
    }


    internal class Font
    {
        public const string RegistryPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts";

        [DllImport("gdi32", EntryPoint = "AddFontResource")]
        public static extern int AddFontResourceA(string lpFileName);
        [DllImport("gdi32.dll")]
        private static extern int AddFontResource(string lpszFilename);
        [DllImport("gdi32.dll")]
        private static extern int CreateScalableFontResource(
            uint fdwHidden, string lpszFontRes, string lpszFontFile, string lpszCurrentPath);

        public static void Install(string fontPath)
        {
            string fontFileName = Path.GetFileName(fontPath);
            string osFontsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            string fontDestination = Path.Combine(osFontsFolder, fontFileName);
            string currentDir = Directory.GetCurrentDirectory();

            if (File.Exists(fontDestination))
            {
                throw new FontAlreadyExistsException(fontDestination);
            }

            File.Copy(Path.Combine(currentDir, fontPath), fontDestination);

            PrivateFontCollection fontCol = new PrivateFontCollection();
            fontCol.AddFontFile(fontDestination);
            string actualFontName = fontCol.Families[0].Name;

            AddFontResource(fontDestination);

            Registry.SetValue(RegistryPath, actualFontName, fontFileName, RegistryValueKind.String);
        }
    }
}
