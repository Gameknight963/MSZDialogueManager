using System.Runtime.InteropServices;

namespace MSZDialougeManager
{
    public class ConsoleHelper
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeConsole();

        public static void ShowConsole() => AllocConsole();
    }
}
