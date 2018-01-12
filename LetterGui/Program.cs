using System;
using System.Runtime.InteropServices;
using System.Windows;
using LetterLib.Data;
using LetterLib.Utility;

namespace LetterGui {
    public class Program {
        public static string FPath;
        public static bool NeedQuit;

        [STAThread]
        public static void Main(string[] args) {
            SetConsoleCtrlHandler(ConsoleCtrlCheck, true);
            try {
                FPath = args[0];
                TemplateHelper.Init(FPath);
            }
            catch (Exception) {
                MessageBox.Show("A template file is required");
                return;
            }
            var app = new App();
            app.Run(new EditWindow());

            if (NeedQuit) WordHelper.SafeQuit();
        }


        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(HandlerRoutine handler, bool add);

        public delegate bool HandlerRoutine(CtrlTypes ctrlType);

        public enum CtrlTypes {
            CtrlCEvent = 0,
            CtrlBreakEvent,
            CtrlCloseEvent,
            CtrlLogoffEvent = 5,
            CtrlShutdownEvent
        }

        private static bool ConsoleCtrlCheck(CtrlTypes ctrlType) {
            if (NeedQuit) WordHelper.SafeQuit();
            return true;
        }
    }
}
