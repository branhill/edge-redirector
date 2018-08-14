using EdgeRedirector.Shared;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Web;

namespace EdgeRedirector.Core
{
    public static class Handler
    {
        public static string Handle(string uri)
        {
            try
            {
                Settings settings = Settings.Open();

                string originalUrl = GetOriginalUrl(uri);
                string redirectedUrl = GetRedirectedUrl(originalUrl, settings.SearchEngine);
                StartBrowser(redirectedUrl, settings.Browser);
            }
            catch (Exception e)
            {
                return $"{e.GetType().FullName}: {e.Message}{Environment.NewLine}" +
                       $"{e.StackTrace}{Environment.NewLine}" +
                       $"URI: {uri}";
            }

            return null;
        }

        public static string GetOriginalUrl(string uriString)
        {
            var uri = new Uri(uriString);

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
            return searchQuery is null
                ? originalUrl
                : searchEngine.Replace("%s", HttpUtility.UrlEncode(searchQuery));
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
