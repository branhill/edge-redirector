using EdgeRedirector.Shared;
using Microsoft.Win32;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text.RegularExpressions;
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

        public static string GetDefaultBrowser()
        {
            if (Registry.GetValue(
                @"HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Associations\UrlAssociations\https\UserChoice",
                "ProgId", null) is string progId)
            {
                if (Registry.GetValue($@"HKEY_CLASSES_ROOT\{progId}\shell\open\command", null, null) is string command)
                {
                    Match match = Regex.Match(command, "\"(.*?)\"");
                    if (match.Success)
                        return match.Groups[1].Value;

                    int exeIndex = command.LastIndexOf(".exe", StringComparison.OrdinalIgnoreCase);
                    if (exeIndex != -1)
                        return command.Substring(0, exeIndex + 4);
                }
            }

            throw new Exception("Unable to get the system default browser. Please configure it in the settings.");
        }

        public static void StartBrowser(string url, string browser = null)
        {
            if (string.IsNullOrWhiteSpace(browser))
                browser = GetDefaultBrowser();

            Process.Start(browser, url);
        }
    }
}
