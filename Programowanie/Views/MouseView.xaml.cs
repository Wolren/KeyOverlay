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
        if (Default.MouseCPS) MouseCPS.IsChecked = true;
        if (Default.ScrollClicks) ScrollClicks.IsChecked = true;
        if (Default.ScrollCPS) ScrollCPS.IsChecked = true;
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
        MouseCPS.IsChecked = true;
        ScrollClicks.IsChecked = true;
        ScrollCPS.IsChecked = true;
    }

    private void mouseStatisticsUncheck(object sender, RoutedEventArgs e)
    {
        MouseClicks.IsChecked = false;
        MouseCPS.IsChecked = false;
        ScrollClicks.IsChecked = false;
        ScrollCPS.IsChecked = false;
    }

    private void scrollClicksCheck(object sender, RoutedEventArgs e)
    {
        Default.ScrollClicks = true;
    }

    private void scrollClicksUncheck(object sender, RoutedEventArgs e)
    {
        Default.ScrollClicks = false;
    }

    private void mouseClicksCheck(object sender, RoutedEventArgs e)
    {
        Default.MouseClicks = true;
    }

    private void mouseClicksUncheck(object sender, RoutedEventArgs e)
    {
        Default.MouseClicks = false;
    }

    private void scrollCpsCheck(object sender, RoutedEventArgs e)
    {
        Default.ScrollCPS = true;
    }

    private void scrollCpsUncheck(object sender, RoutedEventArgs e)
    {
        Default.ScrollCPS = false;
    }

    private void mouseCpsCheck(object sender, RoutedEventArgs e)
    {
        Default.MouseCPS = true;
    }

    private void mouseCpsUncheck(object sender, RoutedEventArgs e)
    {
        Default.MouseCPS = false;
    }
}