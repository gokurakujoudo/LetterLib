using System;
using System.Windows;
using LetterLib.Data;

namespace LetterGui
{
    public class Program {
        public static string fPath;

        [STAThread]
        public static void Main(string[] args) {
            try {
                fPath = args[0];
                TemplateHelper.Init(fPath);
            }
            catch (Exception e) {
                MessageBox.Show("A template file is required");
                return;
            }
            var app = new App();
            app.Run(new EditWindow());
        }
    }
}
