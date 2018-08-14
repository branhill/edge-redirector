using System;
using System.IO;

namespace EdgeRedirector.Core
{
    public class Settings
    {
        private static readonly string DefaultPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EdgeRedirector", "Settings");

        public string Browser { get; set; }

        public string SearchEngine { get; set; }

        public static Settings Open(string path = null)
        {
            if (path is null)
                path = DefaultPath;

            var settings = new Settings();
            if (!File.Exists(path))
                return settings;

            string[] lines = File.ReadAllLines(path);
            if (lines.Length > 0)
                settings.Browser = lines[0];
            if (lines.Length > 1)
                settings.SearchEngine = lines[1];
            return settings;
        }

        public void Save(string path = null)
        {
            if (path is null)
                path = DefaultPath;

            string parentDirectory = Directory.GetParent(path).FullName;
            Directory.CreateDirectory(parentDirectory);

            string[] lines =
            {
                Browser,
                SearchEngine
            };
            File.WriteAllLines(path, lines);
        }
    }
}
