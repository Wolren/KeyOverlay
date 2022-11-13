using System.Linq;
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
        if (!IsWindowOpen<KeyboardOverlay>())
        {
            KeyboardOverlay keyboardOverlay = new KeyboardOverlay();
            keyboardOverlay.Show();
        }
    }
    
    public static bool IsWindowOpen<T>(string name = "") where T : Window
    {
        return string.IsNullOrEmpty(name)
            ? Application.Current.Windows.OfType<T>().Any()
            : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
    }
}