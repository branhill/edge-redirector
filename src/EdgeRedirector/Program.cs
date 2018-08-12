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
                Redirect(args[0]);
            }
            else
            {
                Gui.EntryPoint.Main();
            }
        }

        private static void Redirect(string url)
        {

        }
    }
}
