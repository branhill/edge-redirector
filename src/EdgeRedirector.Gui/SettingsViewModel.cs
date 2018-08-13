using EdgeRedirector.Core;
using EdgeRedirector.Gui.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

namespace EdgeRedirector.Gui
{
    public class SettingsViewModel : Observable
    {
        private readonly Settings _settings;
        private readonly DispatcherTimer _lazySaveTimer;
        private string _selectedSearchEngine;

        public SettingsViewModel(Settings settings)
        {
            _settings = settings;

            _lazySaveTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1000) };
            _lazySaveTimer.Tick += (s, e) => { SaveSettings(); };

            if (string.IsNullOrWhiteSpace(_settings.SearchEngine))
            {
                _selectedSearchEngine = "0";
            }
            else
            {
                _selectedSearchEngine = SearchEngines.SingleOrDefault(s =>
                    string.Equals(s.Value, _settings.SearchEngine, StringComparison.OrdinalIgnoreCase)).Key;
            }
        }

        public string Browser
        {
            get => _settings.Browser;
            set
            {
                Set(_settings.Browser, value, () => _settings.Browser = value);
                LazySaveSettings();
            }
        }

        public string SearchEngine
        {
            get => _settings.SearchEngine;
            set
            {
                Set(_settings.SearchEngine, value, () => _settings.SearchEngine = value);
                LazySaveSettings();
            }
        }

        public string SelectedSearchEngine
        {
            get => _selectedSearchEngine;
            set
            {
                Set(ref _selectedSearchEngine, value);
                if (!(value is null))
                    SearchEngine = SearchEngines[value];
            }
        }

        public Dictionary<string, string> SearchEngines { get; } = new Dictionary<string, string>
        {
            { "0", "" },
            { "1", "https://www.google.com/search?q=%s" },
            { "2", "https://search.yahoo.com/search?p=%s" },
            { "3", "https://duckduckgo.com/?q=%s" }
        };

        public void SaveSettings()
        {
            _settings.Save();
            _lazySaveTimer.Stop();
        }

        private void LazySaveSettings()
        {
            _lazySaveTimer.Stop();
            _lazySaveTimer.Start();
        }
    }
}