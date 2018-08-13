using System;
using System.Windows;

namespace EdgeRedirector.Gui
{
    public class EntryPoint
    {
        [STAThread]
        public static void Main(Window window = null)
        {
            var app = new Application();
            app.Run(window);
        }

        public static void ShowSettingsWindow()
        {
            Main(new SettingsWindow());
        }

        public static void ShowMessageWindow(string message, string uri = null)
        {
            MessageBox.Show(message + Environment.NewLine + uri, "Edge Redirector", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}
