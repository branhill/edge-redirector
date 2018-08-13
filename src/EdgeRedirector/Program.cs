using System;
using System.Diagnostics;

namespace EdgeRedirector
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                string uri = args[0];
                Debug.WriteLine(uri);
                string result = Core.Handler.Handle(uri);

                if (result is null)
                    return;
                string message = result + Environment.NewLine + Environment.NewLine +
                                 "Please report to https://github.com/branhill/edge-redirector/issues";
                Gui.EntryPoint.ShowMessageWindow(message, "Error occurred");
            }
            else
            {
                Gui.EntryPoint.ShowSettingsWindow();
            }
        }
    }
}
