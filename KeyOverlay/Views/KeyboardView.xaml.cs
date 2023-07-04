using System.Linq;
using System.Windows;
using Programowanie.Overlays;
using static System.Windows.Application;
using static Programowanie.Properties.Settings;

namespace Programowanie.Views;

public partial class KeyboardView
{
    public KeyboardView()
    {
        InitializeComponent();
        if (Default.KeyboardClicks) KeyboardClicks.IsChecked = true;
        if (Default.KeyboardCPS) KeyboardCps.IsChecked = true;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (IsWindowOpen<KeyboardOverlay>()) return;
        KeyboardOverlay keyboardOverlay = new KeyboardOverlay();
        keyboardOverlay.Show();
        if (Default.Close && Current.MainWindow != null) Current.MainWindow.Hide();
    }

    public static bool IsWindowOpen<T>(string name = "") where T : Window
    {
        return string.IsNullOrEmpty(name)
            ? Current.Windows.OfType<T>().Any()
            : Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
    }

    private void KeyboardClicksCheck(object sender, RoutedEventArgs e)
    {
        Default.KeyboardClicks = true;
        Default.Save();
    }

    private void KeyboardClicksUncheck(object sender, RoutedEventArgs e)
    {
        Default.KeyboardClicks = false;
        Default.Save();
    }

    private void KeyboardCpsCheck(object sender, RoutedEventArgs e)
    {
        Default.KeyboardCPS = true;
        Default.Save();
    }

    private void KeyboardCpsUncheck(object sender, RoutedEventArgs e)
    {
        Default.KeyboardCPS = false;
        Default.Save();
    }
}