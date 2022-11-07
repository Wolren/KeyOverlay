using System.Windows;
using System.Windows.Controls;
using Programowanie.Overlays;

namespace Programowanie.Views;

public partial class KeyboardView : UserControl
{
    public KeyboardView()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        KeyboardOverlay keyboardOverlay = new KeyboardOverlay();
        keyboardOverlay.Show();
    }
}