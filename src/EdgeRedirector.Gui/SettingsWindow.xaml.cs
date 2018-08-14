using EdgeRedirector.Shared;
using Microsoft.Win32;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

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

            if (ViewModel.SelectedSearchEngine is null)
            {
                SearchEngineCustomRadioButton.IsChecked = true;
            }
            else
            {
                SearchEnginesItemsControl.Items.Cast<object>()
                    .Select(i => SearchEnginesItemsControl.ItemContainerGenerator.ContainerFromItem(i))
                    .Select(c => (RadioButton)VisualTreeHelper.GetChild(c, 0))
                    .Single(r => (string)r.Tag == ViewModel.SelectedSearchEngine)
                    .IsChecked = true;
            }
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
            ViewModel.SelectedSearchEngine = ((RadioButton)sender).Tag as string;
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
