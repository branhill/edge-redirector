using System;
using System.Windows;

namespace EdgeRedirector.Gui
{
    public class EntryPoint
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new SettingsWindow());
        }
    }
}
