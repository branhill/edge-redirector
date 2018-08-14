using System;
using System.Diagnostics;

namespace EdgeRedirector.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                string uri = args[0];
                Debug.WriteLine(uri);
                string result = Handler.Handle(uri);

                if (result is null)
                    return;
                string message = result + Environment.NewLine + Environment.NewLine +
                                 "Please report to https://github.com/branhill/edge-redirector/issues";
                Process.Start("EdgeRedirector.Gui.exe");
            }
            else
            {
                Process.Start("EdgeRedirector.Gui.exe");
            }
        }
    }
}
