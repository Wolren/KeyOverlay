using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using static Programowanie.Properties.Settings;

namespace Programowanie.Overlays;

[Localizable(false)]
public partial class MouseOverlay
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc callback, IntPtr instance,
        uint threadId);

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr instance);

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr idHook, int code, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string? moduleName);

    public delegate void KeyEventHandler(IntPtr wParam);

    public static event KeyEventHandler? OnKeyPressed;

    public delegate void ScrollEventHandler();

    public static event ScrollEventHandler? OnScroll;

    private static readonly LowLevelMouseProc _proc = hookProc;
    private static IntPtr _hook = IntPtr.Zero;
    private readonly DispatcherTimer _afk = new();
    private readonly DispatcherTimer _timer = new();
    private readonly DispatcherTimer _timer2 = new();
    private static readonly List<int> _keysDown = new();
    private List<double> _current = new();
    private List<double> _current2 = new();
    private readonly List<TextBlock> _lines;
    private double _clicks, _scrolls, _total, _total2, _intervalClicks, _intervalScrolls;

    private enum MouseKeys
    {
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_MOUSEWHEEL = 0x020A,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205
    }

    [Localizable(false)]
    public MouseOverlay()
    {
        InitializeComponent();
        if (!Default.MouseClicks)
        {
            CLICKS.Visibility = Visibility.Collapsed;
            Clicks.Visibility = Visibility.Collapsed;
        }

        if (!Default.MouseCPS)
        {
            CPS.Visibility = Visibility.Collapsed;
            Cps.Visibility = Visibility.Collapsed;
        }

        if (!Default.ScrollClicks)
        {
            SCROLLS.Visibility = Visibility.Collapsed;
            Scrolls.Visibility = Visibility.Collapsed;
        }

        if (!Default.ScrollCPS)
        {
            SPS.Visibility = Visibility.Collapsed;
            Sps.Visibility = Visibility.Collapsed;
        }

        OnKeyPressed += keyHandler;
        OnScroll += scrollHandler;
        _timer.Tick += timerTick;
        _timer2.Tick += timer2Tick;
        _afk.Tick += endTick;
        _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        _timer2.Interval = new TimeSpan(0, 0, 0, 0, 100);
        _afk.Interval = new TimeSpan(0, 0, 2);
        Clicks.Text = "0";
        Cps.Text = "0";
        Scrolls.Text = "0";
        Cps.FontSize = 20;
        Clicks.FontSize = 20;
        Scrolls.FontSize = 20;
        Sps.Text = "0";
        Sps.FontSize = 20;
        _lines = new List<TextBlock>
        {
            LC, RC
        };
    }

    private static IntPtr hookProc(int code, IntPtr wParam, IntPtr lParam)
    {
        if (code < 0) return CallNextHookEx(_hook, code, wParam, lParam);
        int vk = Marshal.ReadInt32(lParam);
        if (wParam == (IntPtr)MouseKeys.WM_LBUTTONDOWN || wParam == (IntPtr)MouseKeys.WM_RBUTTONDOWN)
        {
            if (!_keysDown.Contains(vk)) OnKeyPressed?.Invoke(wParam);
            _keysDown.Add(vk);
        }
        else if (wParam == (IntPtr)MouseKeys.WM_LBUTTONUP || wParam == (IntPtr)MouseKeys.WM_RBUTTONUP)
        {
            _keysDown.RemoveAll(k => k == vk);
        }
        else if (wParam == (IntPtr)MouseKeys.WM_MOUSEWHEEL)
        {
            OnScroll?.Invoke();
        }

        return CallNextHookEx(_hook, code, wParam, lParam);
    }

    private void keyHandler(IntPtr wParam)
    {
        SolidColorBrush brush = new SolidColorBrush(Colors.Red);
        if (!_timer.IsEnabled) _timer.Start();
        _afk.Stop();
        if (wParam == (IntPtr)MouseKeys.WM_LBUTTONDOWN) LC.Background = brush;
        if (wParam == (IntPtr)MouseKeys.WM_RBUTTONDOWN) RC.Background = brush;
        _clicks++;
        _intervalClicks++;
        Clicks.Text = _clicks.ToString(CultureInfo.InvariantCulture);
        _afk.Start();
    }

    private void scrollHandler()
    {
        if (!_timer2.IsEnabled) _timer2.Start();
        _afk.Stop();
        SW.Background = new SolidColorBrush(Colors.Red);
        _scrolls++;
        _intervalScrolls++;
        Scrolls.Text = _scrolls.ToString(CultureInfo.InvariantCulture);
        _afk.Start();
    }

    private void timerTick(object? sender, EventArgs e)
    {
        _current.Add(_intervalClicks);
        _total += _intervalClicks;
        if (_current.Count > 10)
        {
            _current = _current.Skip(1).ToList();
            _total -= _current[0];
        }

        _intervalClicks = 0;
        Cps.Text = Math.Round(_total, 0).ToString(CultureInfo.InvariantCulture);
        backgroundReset();
    }

    private void timer2Tick(object? sender, EventArgs e)
    {
        _current2.Add(_intervalScrolls);
        _total2 += _intervalScrolls;
        if (_current2.Count > 10)
        {
            _current2 = _current2.Skip(1).ToList();
            _total2 = _current2[0];
        }

        _intervalScrolls = 0;
        Sps.Text = Math.Round(_total2, 0).ToString(CultureInfo.InvariantCulture);
        SW.Background = new SolidColorBrush(Color.FromRgb(161, 182, 200));
    }

    private void backgroundReset()
    {
        foreach (TextBlock l in _lines) l.Background = new SolidColorBrush(Color.FromRgb(161, 182, 200));
    }

    private void endTick(object? sender, EventArgs e)
    {
        Cps.Text = "0";
        Sps.Text = "0";
        _total = 0;
        _total2 = 0;
        _current.Clear();
        _current2.Clear();
        _timer.Stop();
        _afk.Stop();
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        DragMove();
    }

    private static void setHook()
    {
        using Process curProcess = Process.GetCurrentProcess();
        using ProcessModule? curModule = curProcess.MainModule;
        _hook = SetWindowsHookEx(14, _proc, GetModuleHandle(curModule?.ModuleName), 0);
    }

    private static void unHook()
    {
        UnhookWindowsHookEx(_hook);
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        setHook();
    }

    protected override void OnClosed(EventArgs e)
    {
        if (Application.Current.MainWindow != null) Application.Current.MainWindow.Show();
        unHook();
    }

    private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
}