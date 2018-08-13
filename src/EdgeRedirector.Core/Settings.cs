using Newtonsoft.Json;
using System;
using System.IO;

namespace EdgeRedirector.Core
{
    public class Settings
    {
        private static readonly JsonSerializer JsonSerializer = new JsonSerializer { Formatting = Formatting.Indented };

        private static readonly string DefaultPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "EdgeRedirector", "Settings.json");

        public string Browser { get; set; }

        public string SearchEngine { get; set; }

        public static Settings Open(string path = null)
        {
            if (path is null)
                path = DefaultPath;

            if (!File.Exists(path))
                return new Settings();

            using (var streamReader = new StreamReader(path))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                return JsonSerializer.Deserialize<Settings>(jsonTextReader);
            }
        }

        public void Save(string path = null)
        {
            if (path is null)
                path = DefaultPath;

            string parentDirectory = Directory.GetParent(path).FullName;
            Directory.CreateDirectory(parentDirectory);

            using (var streamWriter = new StreamWriter(path))
            using (var jsonTextWriter = new JsonTextWriter(streamWriter))
            {
                JsonSerializer.Serialize(jsonTextWriter, this);
            }
        }
    }
}
