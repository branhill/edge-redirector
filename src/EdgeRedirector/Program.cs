using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Web;

namespace EdgeRedirector
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                string result = Handle(args[0]);
                if (result != string.Empty)
                    Gui.EntryPoint.ShowMessageWindow(result);
            }
            else
            {
                Gui.EntryPoint.ShowSettingsWindow();
            }
        }

        public static string Handle(string arg)
        {
            Debug.WriteLine(arg);

            try
            {
                string url = GetUrl(arg);
                Process.Start(url);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return string.Empty;
        }

        public static string GetUrl(string edgeUri)
        {
            var uri = new Uri(edgeUri);

            if (!string.Equals(uri.Scheme, "microsoft-edge", StringComparison.OrdinalIgnoreCase))
                throw new Exception("The scheme is not microsoft-edge.");

            if (!string.IsNullOrEmpty(uri.AbsolutePath))
                return uri.PathAndQuery;

            NameValueCollection query = HttpUtility.ParseQueryString(uri.Query);
            string queryUrl = query["url"];
            if (queryUrl is null)
                throw new Exception("Can not get url from query string.");
            return queryUrl;
        }
    }
}
