using Programowanie.Overlays;
using System.Windows;
using System.Windows.Controls;

namespace Programowanie.Views;

public partial class MouseView : UserControl
{
    public MouseView()
    {
        InitializeComponent();
    }
    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (!KeyboardView.IsWindowOpen<MouseOverlay>())
        {
            MouseOverlay mouseOverlay = new MouseOverlay();
            mouseOverlay.Show();
        }
    }
}