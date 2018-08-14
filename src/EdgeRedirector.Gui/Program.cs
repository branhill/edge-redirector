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
                MessageBox.Show(args[1], "Edge Redirector", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var app = new Application();
                app.Run(new SettingsWindow());
            }
        }
    }
}