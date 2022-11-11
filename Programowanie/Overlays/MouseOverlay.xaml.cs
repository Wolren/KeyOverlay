using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Programowanie.Overlays;

public partial class MouseOverlay : Window
{
    private readonly DispatcherTimer _afk = new();
    private readonly DispatcherTimer _timer = new();
    private double _clicks, _total, _intervalClicks;
    private List<double> _cur = new();

    public MouseOverlay()
    {
        InitializeComponent();
        MouseDown += Mouse_Click;
        _timer.Tick += TimerTick;
        _afk.Tick += EndTick;
        _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        _afk.Interval = new TimeSpan(0, 0, 2);
        Clicks.Text = "0";
        Cps.Text = "0";
        Cps.FontSize = 20;
        Clicks.FontSize = 20;
    }
    private void Mouse_Click (object sender, MouseEventArgs e)
    {
        BackgroundSwitch();
        if (!_timer.IsEnabled) _timer.Start();
        _afk.Stop();
        _clicks++;
        _intervalClicks++;
        Clicks.Text = _clicks.ToString(CultureInfo.InvariantCulture);
        _afk.Start();
    }

    private void TimerTick(object? sender, EventArgs e)
    {
        _cur.Add(_intervalClicks);
        _total += _intervalClicks;
        if (_cur.Count > 10)
        {
            _cur = _cur.Skip(1).ToList();
            _total -= _cur[0];
        }
        _intervalClicks = 0;
        Cps.Text = Math.Round(_total, 0).ToString(CultureInfo.InvariantCulture);
        BackgroundReset();
    }
    private void EndTick(object? sender, EventArgs e)
    {
        Cps.Text = "0";
        _total = 0;
        _cur.Clear();
        _timer.Stop();
        _afk.Stop();
    }

    private void BackgroundSwitch()
    {
        var brush = new SolidColorBrush(Colors.Red);
        if (Mouse.LeftButton == MouseButtonState.Pressed) LC.Background = brush;
        else if (Mouse.MiddleButton == MouseButtonState.Pressed) SW.Background = brush;
        else if (Mouse.RightButton == MouseButtonState.Pressed) RC.Background = brush;
    }
    private void BackgroundReset()
    {
        var brush = new SolidColorBrush(Color.FromRgb(161, 182, 200));
        List<TextBlock> lines = new() {
            LC, SW, RC
        };
        foreach (var l in lines) { l.Background = brush; }
    }
}