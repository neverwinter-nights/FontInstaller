using Microsoft.Win32;
using System;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace FontInstaller
{
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

            PrivateFontCollection fontCollection = new PrivateFontCollection();
            fontCollection.AddFontFile(fontDestination);
            string actualFontName = fontCollection.Families[0].Name;

            AddFontResource(fontDestination);

            Registry.SetValue(RegistryPath, actualFontName, fontFileName, RegistryValueKind.String);
        }
    }
}
