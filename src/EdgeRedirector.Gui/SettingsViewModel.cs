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

        private bool _isInstalled;
        private RelayCommand _installCommand;
        private bool _installCommandCanExecute = true;

        public SettingsViewModel(Settings settings)
        {
            _settings = settings;
            IsInAppContainer = PlatformDetection.IsInAppContainer();
            if (!IsInAppContainer)
                CheckIsInstalled();

            _lazySaveTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1000) };
            _lazySaveTimer.Tick += (s, e) => SaveSettings();

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

        public bool IsInAppContainer { get; }

        public bool IsNotInAppContainer => !IsInAppContainer;

        public bool IsInstalled
        {
            get => _isInstalled;
            set => Set(ref _isInstalled, value);
        }

        public RelayCommand InstallCommand => _installCommand ?? (_installCommand = new RelayCommand(
            () =>
            {
                _installCommandCanExecute = false;
                _installCommand.OnCanExecuteChanged();

                if (IsInstalled)
                {
                }
                else
                {
                }

                IsInstalled = !IsInstalled;

                _installCommandCanExecute = true;
                _installCommand.OnCanExecuteChanged();
            },
            () => _installCommandCanExecute));

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

        private void CheckIsInstalled()
        {

        }

        private void SettingsViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Browser):
                case nameof(SearchEngine):
                    LazySaveSettings();
                    break;
            }
        }
    }
}