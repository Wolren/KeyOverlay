using System.Windows;
using static Programowanie.Properties.Settings;

namespace Programowanie.Views;

public partial class SettingsView
{
    public SettingsView()
    {
        InitializeComponent();
        if (Default.Close.Equals(true)) Closed.IsChecked = true;
    }

    private void closeCheck(object sender, RoutedEventArgs e)
    {
        Default.Close = true;
        Default.Save();
    }

    private void closeUncheck(object sender, RoutedEventArgs e)
    {
        Default.Close = false;
        Default.Save();
    }
}