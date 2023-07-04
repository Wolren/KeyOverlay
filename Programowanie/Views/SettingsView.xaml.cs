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

    private void CloseCheck(object sender, RoutedEventArgs e)
    {
        Default.Close = true;
        Default.Save();
    }

    private void CloseUncheck(object sender, RoutedEventArgs e)
    {
        Default.Close = false;
        Default.Save();
    }
}