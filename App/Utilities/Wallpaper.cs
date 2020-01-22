using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace BingDesktop.Utilities
{
    /// <summary>
    /// Based on https://stackoverflow.com/questions/1061678/change-desktop-wallpaper-using-code-in-net
    /// </summary>
    public static class Wallpaper
    {
        private const int SPI_SETDESKWALLPAPER = 20;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public enum Style : int
        {
            Center = 0,
            Stretch = 2,
            Fit = 6,
            Fill = 10,
            Span = 22
        }

        public static void Set(string tempPath, Style style = Style.Fill)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            key.SetValue(@"WallpaperStyle", (int) style);

            SystemParametersInfo(SPI_SETDESKWALLPAPER,
                0,
                tempPath,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
    }
}
