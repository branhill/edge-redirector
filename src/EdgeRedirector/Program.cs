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
                if (!(result is null))
                    Gui.EntryPoint.ShowMessageWindow($"{result}{Environment.NewLine}{Environment.NewLine}URI: {uri}");
            }
            else
            {
                Gui.EntryPoint.ShowSettingsWindow();
            }
        }
    }
}
