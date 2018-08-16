using System;
using System.Diagnostics;

namespace EdgeRedirector.Core
{
    public class Program
    {
        private const string GuiFileName = "EdgeRedirector.UI.exe";

        public static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                string uri = args[0];

                string result = Handler.Handle(uri);

                if (result is null)
                    return;
                string message = result + Environment.NewLine + Environment.NewLine +
                                 "Please report to https://github.com/branhill/edge-redirector/issues";
                Process.Start(GuiFileName, $"message {Uri.EscapeDataString(message)}");
            }
            else
            {
                Process.Start(GuiFileName);
            }
        }
    }
}
