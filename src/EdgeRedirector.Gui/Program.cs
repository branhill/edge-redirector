using System;
using System.Windows;

namespace EdgeRedirector.Gui
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if (args.Length == 2 && string.Equals(args[0], "message", StringComparison.OrdinalIgnoreCase))
            {
                string message = Uri.UnescapeDataString(args[1]);
                MessageBox.Show(message, "Edge Redirector", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var app = new Application();
                app.Run(new SettingsWindow());
            }
        }
    }
}