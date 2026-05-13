using Microsoft.Win32;

namespace MSZDialougeManager
{
    public static class AssociationHelper
    {
        public static void RegisterFileAssociation()
        {
            string exePath = Application.ExecutablePath;
            Registry.SetValue($@"HKEY_CURRENT_USER\Software\Classes\.{FilesystemManager.ext}", "", "mszdlg.file");
            Registry.SetValue($@"HKEY_CURRENT_USER\Software\Classes\mszdlg.file", "", "Miside Zero Dialogue Pack");
            Registry.SetValue($@"HKEY_CURRENT_USER\Software\Classes\mszdlg.file\shell\open\command", "", $"\"{exePath}\" \"%1\"");
        }

        public static void UnregisterFileAssociation()
        {
            Registry.CurrentUser.DeleteSubKeyTree($@"Software\Classes\.{FilesystemManager.ext}", false);
            Registry.CurrentUser.DeleteSubKeyTree($@"Software\Classes\mszdlg.file", false);
        }

        public static bool IsFileAssociationRegistered()
        {
            return Registry.GetValue($@"HKEY_CURRENT_USER\Software\Classes\.{FilesystemManager.ext}", "", null) != null;
        }

        public static bool IsFileAssociationCurrent()
        {
            object? value = Registry.GetValue(
                @"HKEY_CURRENT_USER\Software\Classes\mszdlg\shell\open\command", "", null);
            if (value == null) return false;
            return value.ToString()!.Contains(Application.ExecutablePath);
        }
    }
}
