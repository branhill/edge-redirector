using System;
using System.Diagnostics;
using System.IO;

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
                if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GuiFileName)))
                    Process.Start(GuiFileName, $"message {Uri.EscapeDataString(message)}");
            }
            else
            {
                if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GuiFileName)))
                    Process.Start(GuiFileName);
            }
        }
    }
}
