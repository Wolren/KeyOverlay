using Programowanie.Overlays;
using System.Windows;
using static System.Windows.Application;
using static Programowanie.Properties.Settings;

namespace Programowanie.Views;

public partial class MouseView
{
    public MouseView()
    {
        InitializeComponent();
        if (Default.MouseClicks) MouseClicks.IsChecked = true;
        if (Default.MouseCPS) MouseCps.IsChecked = true;
        if (Default.ScrollClicks) ScrollClicks.IsChecked = true;
        if (Default.ScrollCPS) ScrollCps.IsChecked = true;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (KeyboardView.isWindowOpen<MouseOverlay>()) return;
        MouseOverlay mouseOverlay = new MouseOverlay();
        mouseOverlay.Show();
        if (Default.Close && Current.MainWindow != null)
        {
            Current.MainWindow.Hide();
        }
    }

    private void mouseStatisticsCheck(object sender, RoutedEventArgs e)
    {
        MouseClicks.IsChecked = true;
        MouseCps.IsChecked = true;
        ScrollClicks.IsChecked = true;
        ScrollCps.IsChecked = true;
    }

    private void mouseStatisticsUncheck(object sender, RoutedEventArgs e)
    {
        MouseClicks.IsChecked = false;
        MouseCps.IsChecked = false;
        ScrollClicks.IsChecked = false;
        ScrollCps.IsChecked = false;
    }

    private void scrollClicksCheck(object sender, RoutedEventArgs e)
    {
        Default.ScrollClicks = true;
        Default.Save();
    }

    private void scrollClicksUncheck(object sender, RoutedEventArgs e)
    {
        Default.ScrollClicks = false;
        Default.Save();
    }

    private void mouseClicksCheck(object sender, RoutedEventArgs e)
    {
        Default.MouseClicks = true;
        Default.Save();
    }

    private void mouseClicksUncheck(object sender, RoutedEventArgs e)
    {
        Default.MouseClicks = false;
        Default.Save();
    }

    private void scrollCpsCheck(object sender, RoutedEventArgs e)
    {
        Default.ScrollCPS = true;
        Default.Save();
    }

    private void scrollCpsUncheck(object sender, RoutedEventArgs e)
    {
        Default.ScrollCPS = false;
        Default.Save();
    }

    private void mouseCpsCheck(object sender, RoutedEventArgs e)
    {
        Default.MouseCPS = true;
        Default.Save();
    }

    private void mouseCpsUncheck(object sender, RoutedEventArgs e)
    {
        Default.MouseCPS = false;
        Default.Save();
    }
}