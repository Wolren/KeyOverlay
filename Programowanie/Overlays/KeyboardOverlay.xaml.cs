using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Programowanie.Overlays;

public partial class KeyboardOverlay : Window
{
    private readonly DispatcherTimer _afk = new();
    private readonly DispatcherTimer _timer = new();
    private double _clicks, _total, _intervalClicks;
    private List<double> _cur = new();

    public KeyboardOverlay()
    {
        KeyDown += Window_KeyDown;
        _timer.Tick += TimerTick;
        _afk.Tick += EndTick;
        _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        _afk.Interval = new TimeSpan(0, 0, 2);
        Clicks.Text = "0";
        Cps.Text = "0";
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        DragMove();
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if (!e.IsRepeat)
        {
            if (!_timer.IsEnabled) _timer.Start();
            _afk.Stop();
            _clicks++;
            _intervalClicks++;
            Clicks.Text = _clicks.ToString(CultureInfo.InvariantCulture);
            _afk.Start();
        }
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
    }

    private void EndTick(object? sender, EventArgs e)
    {
        Cps.Text = "0";
        _total = 0;
        _cur.Clear();
        _timer.Stop();
        _afk.Stop();
    }
}