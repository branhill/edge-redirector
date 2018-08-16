using EdgeRedirector.Shared;
using Microsoft.Win32;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace EdgeRedirector.Gui
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            ViewModel = new SettingsViewModel(Settings.Open());

            InitializeComponent();
        }

        public SettingsViewModel ViewModel { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ViewModel.Browser))
                BrowserDefaultRadioButton.IsChecked = true;
            else
                BrowserCustomRadioButton.IsChecked = true;

            if (string.IsNullOrWhiteSpace(ViewModel.SearchEngine))
                ViewModel.SearchEngine = string.Empty;
            RadioButton selectedSearch = SearchEngineStackPanel.Children.OfType<RadioButton>()
                .SingleOrDefault(r => (string)r.Tag == ViewModel.SearchEngine) ?? SearchEngineCustomRadioButton;
            selectedSearch.IsChecked = true;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (Keyboard.FocusedElement is TextBox textBox)
            {
                BindingExpression bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);
                if (bindingExpression != null && textBox.IsEnabled && !textBox.IsReadOnly)
                    bindingExpression.UpdateSource();
            }

            ViewModel.SaveSettings();
        }

        private void BrowserDefaultRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.Browser = string.Empty;
        }

        private void SearchEngineRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (((RadioButton)sender).Tag is string url)
                ViewModel.SearchEngine = url;
        }

        private void BrowserOpenButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Programs (*.exe;*.pif;*.com;*.bat;*.cmd)|*.exe;*.pif;*.com;*.bat;*.cmd|" +
                         "All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
                ViewModel.Browser = openFileDialog.FileName;
        }
    }
}
