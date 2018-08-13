using System.Windows;

namespace EdgeRedirector.Gui
{
    public class EntryPoint
    {
        private static void Main(Window window)
        {
            var app = new Application();
            app.Run(window);
        }

        public static void ShowSettingsWindow()
        {
            Main(new SettingsWindow());
        }

        public static void ShowMessageWindow(string message)
        {
            MessageBox.Show(message, "Edge Redirector", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
