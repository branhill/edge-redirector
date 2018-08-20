using System;
using System.IO;

namespace EdgeRedirector.Shared
{
    public class Settings
    {
        private const string FileName = "Settings";
        private string _storagePath;

        public Settings() { }

        public Settings(string storagePath)
        {
            _storagePath = storagePath;
        }

        public string Browser { get; set; } = string.Empty;

        public string SearchEngine { get; set; } = string.Empty;

        public string StoragePath => _storagePath ?? (_storagePath = GetStoragePath());

        public static Settings Open()
        {
            string path = GetStoragePath();
            var settings = new Settings(path);
            if (!File.Exists(path))
                return settings;

            string[] lines = File.ReadAllLines(path);
            if (lines.Length > 0)
                settings.Browser = lines[0];
            if (lines.Length > 1)
                settings.SearchEngine = lines[1];
            return settings;
        }

        public void Save()
        {
            string parentDirectory = Directory.GetParent(StoragePath).FullName;
            Directory.CreateDirectory(parentDirectory);

            string[] lines =
            {
                Browser,
                SearchEngine
            };
            File.WriteAllLines(StoragePath, lines);
        }

        private static string GetStoragePath()
        {
            string portablePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName);
            if (File.Exists(portablePath))
                return portablePath;

            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "EdgeRedirector", FileName);
            if (File.Exists(appDataPath))
                return appDataPath;

            try
            {
                using (File.Create(portablePath))
                {
                    return portablePath;
                }
            }
            catch (UnauthorizedAccessException)
            {
                return appDataPath;
            }
        }
    }
}
