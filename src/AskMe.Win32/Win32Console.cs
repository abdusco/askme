using System.Runtime.InteropServices;

namespace AskMe.Win32
{
    public static class Win32Console
    {
        [DllImport("Kernel32.dll")]
        private static extern bool AttachConsole(int processId);

        public static void AttachConsole() => AttachConsole(-1);
    }
}