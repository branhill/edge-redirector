using System;

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
                string result = Core.Handler.Handle(uri);
                if (result != string.Empty)
                    Gui.EntryPoint.ShowMessageWindow(result);
            }
            else
            {
                Gui.EntryPoint.ShowSettingsWindow();
            }
        }
    }
}
