using System;
using System.IO;
using System.Windows;

namespace EdgeRedirector.Gui
{
    public class Program
    {
        private const string MainProgramFileName = "EdgeRedirector.exe";

        [STAThread]
        public static void Main(string[] args)
        {
            if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, MainProgramFileName)))
            {
                MessageBox.Show($"The main program {MainProgramFileName} doesn't exists.", "Edge Redirector",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }

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