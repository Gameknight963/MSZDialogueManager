using MSZDialougeManager.Styling;
using System.Runtime.InteropServices;

namespace MSZDialougeManager
{
    // i guess bro

    public static class ThemeSwitchers
    {
        private static bool scrollHooked = false;

        public static void SetScrollHooked(bool enabled)
        {
            if (scrollHooked != enabled)
            {
                if (enabled) ScrollHook.Install();
                else ScrollHook.Uninstall();
                scrollHooked = enabled;
            }
        }
        public static void SetFormThemeAndStuff(ThemeManager.Theme theme, params Control[] affectedControls)
        {
            SetScrollHooked(theme switch
            {
                ThemeManager.Theme.Blur => true,
                ThemeManager.Theme.Acrylic => true,
                _ => false
            });

            string themeName = theme switch
            {
                ThemeManager.Theme.Light => "Explorer",
                _ => "DarkMode_Explorer"
            };
            foreach (Control control in affectedControls)
            {
                Marshal.ThrowExceptionForHR(SetWindowTheme(control.Handle, themeName, null));
            }
        }

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        static extern int SetWindowTheme(IntPtr hwnd, string? pszSubAppName, string? pszSubIdList);
    }
}
