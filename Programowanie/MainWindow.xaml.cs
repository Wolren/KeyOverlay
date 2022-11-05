using System.Windows;
using Programowanie.ViewModels;

namespace Programowanie
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Height = SystemParameters.PrimaryScreenHeight * 0.60;
            Width = SystemParameters.PrimaryScreenWidth * 0.80;
            DataContext = new MainViewModel();
        }

        private void Minimize_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}