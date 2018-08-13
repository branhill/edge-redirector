using EdgeRedirector.Core;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
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
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "EdgeRedirector", "Settings.json");
                Settings settings = Settings.Open(path);

                string url = GetUrl(arg);
                string redirectedUrl = GetRedirectedUrl(url, settings.SearchEngine);
                StartBrowser(redirectedUrl, settings.Browser);
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

        public static string GetRedirectedUrl(string originalUrl, string searchEngine = null)
        {
            if (string.IsNullOrWhiteSpace(searchEngine) ||
                originalUrl.IndexOf("bing.com", StringComparison.OrdinalIgnoreCase) == -1)
            {
                return originalUrl;
            }

            var uri = new Uri(originalUrl);
            NameValueCollection query = HttpUtility.ParseQueryString(uri.Query);
            string searchQuery = query["q"];
            if (searchQuery is null)
                return originalUrl;
            return searchEngine.Replace("%s", HttpUtility.UrlEncode(searchQuery));
        }

        public static void StartBrowser(string url, string browser = null)
        {
            if (string.IsNullOrWhiteSpace(browser))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            else
            {
                Process.Start(browser, url);
            }
        }
    }
}
