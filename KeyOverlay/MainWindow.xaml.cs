using System.Windows;
using System.Windows.Input;
using Programowanie.ViewModels;

namespace Programowanie;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        Height = SystemParameters.PrimaryScreenHeight * 0.60;
        Width = SystemParameters.PrimaryScreenWidth * 0.80;
        Tops.Width = SystemParameters.PrimaryScreenWidth * 0.75;
        DataContext = new MainViewModel();
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        DragMove();
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