using EdgeRedirector.Gui.Helpers;
using EdgeRedirector.Shared;
using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace EdgeRedirector.Gui
{
    public class SettingsViewModel : Observable
    {
        private readonly Settings _settings;
        private readonly DispatcherTimer _lazySaveTimer;

        public SettingsViewModel(Settings settings)
        {
            _settings = settings;

            _lazySaveTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1000) };
            _lazySaveTimer.Tick += (s, e) => { SaveSettings(); };

            PropertyChanged += SettingsViewModel_PropertyChanged;
        }

        public string Browser
        {
            get => _settings.Browser;
            set => Set(_settings.Browser, value, () => _settings.Browser = value);
        }

        public string SearchEngine
        {
            get => _settings.SearchEngine;
            set => Set(_settings.SearchEngine, value, () => _settings.SearchEngine = value);
        }

        public string StoragePath => _settings.StoragePath;

        public void SaveSettings()
        {
            _lazySaveTimer.Stop();
            _settings.Save();
        }

        private void LazySaveSettings()
        {
            _lazySaveTimer.Stop();
            _lazySaveTimer.Start();
        }

        private void SettingsViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            LazySaveSettings();
        }
    }
}