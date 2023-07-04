using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using static System.Windows.Application;
using static Programowanie.Properties.Settings;

namespace Programowanie.Overlays;

public partial class SingleKeyOverlay
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr instance,
        uint threadId);

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern bool UnhookWindowsHookEx(IntPtr instance);

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr idHook, int code, int wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr LoadLibrary([Localizable(false)] string fileName);

    public delegate void KeyEventHandler(int vk);

    public static event KeyEventHandler? OnKeyPressed;

    private static readonly List<int> _keys = new()
    {
        0x1B, 0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79, 0x7A, 0x7B,
        0xC0, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30, 0xBD, 0xBB,
        0x08, 0x09, 0x51, 0x57, 0x45, 0x52, 0x54, 0x59, 0x55, 0x49, 0x4F, 0x50, 0xDB,
        0xDD, 0xDC, 0x14, 0x41, 0x53, 0x44, 0x46, 0x47, 0x48, 0x4A, 0x4B, 0x4C, 0xBA,
        0xDE, 0x0D, 0xA0, 0x5A, 0x58, 0x43, 0x56, 0x42, 0x4E, 0x4D, 0xBC, 0xBE, 0xBF,
        0xA1, 0xA2, 0x5B, 0xA4, 0x20, 0xA5, 0x5D, 0x11, 0xA3
    };

    private static readonly List<int> _usedKeys = new()
    {
        _keys[Default.Slot1Key], _keys[Default.Slot2Key],
        _keys[Default.Slot3Key], _keys[Default.Slot4Key],
        _keys[Default.Slot5Key]
    };

    private const int WM_KEYDOWN = 0x100;
    private const int WM_KEYUP = 0x101;

    private static readonly LowLevelKeyboardProc _proc = HookProc;
    private static IntPtr _hook = IntPtr.Zero;
    private readonly DispatcherTimer _afk = new();
    private readonly DispatcherTimer _timer = new();
    private static readonly List<int> _keysDown = new();
    private List<double> _current = new();
    private double _clicks, _total, _intervalClicks;
    private readonly List<TextBlock> _slots;

    [Localizable(false)]
    public SingleKeyOverlay()
    {
        InitializeComponent();
        _timer.Tick += TimerTick;
        _afk.Tick += EndTick;
        _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        _afk.Interval = new TimeSpan(0, 0, 2);
        Clicks.Text = "0";
        Cps.Text = "0";
        Cps.FontSize = 20;
        Clicks.FontSize = 20;
        Slot1.Text = Default.Slot1;
        Slot2.Text = Default.Slot2;
        Slot3.Text = Default.Slot3;
        Slot4.Text = Default.Slot4;
        Slot5.Text = Default.Slot5;
        OnKeyPressed += KeyHandler;
        _slots = new List<TextBlock> { Slot1, Slot2, Slot3, Slot4, Slot5 };
    }

    private static IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam)
    {
        if (code < 0) return CallNextHookEx(_hook, code, (int)wParam, lParam);
        int vk = Marshal.ReadInt32(lParam);
        if (wParam == WM_KEYDOWN)
        {
            if (!_keysDown.Contains(vk) && _usedKeys.Contains(vk)) OnKeyPressed?.Invoke(vk);
            _keysDown.Add(vk);
        }
        else if (wParam == WM_KEYUP)
        {
            _keysDown.RemoveAll(k => k == vk);
        }

        return CallNextHookEx(_hook, code, (int)wParam, lParam);
    }

    private void KeyHandler(int vkCode)
    {
        if (!_timer.IsEnabled) _timer.Start();
        _afk.Stop();
        _clicks++;
        _intervalClicks++;
        Clicks.Text = _clicks.ToString(CultureInfo.InvariantCulture);
        BackgroundSwitch(vkCode);
        _afk.Start();
    }

    private void TimerTick(object? sender, EventArgs e)
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
        BackgroundReset();
    }

    [Localizable(false)]
    private void EndTick(object? sender, EventArgs e)
    {
        Cps.Text = "0";
        _total = 0;
        _current.Clear();
        _timer.Stop();
        _afk.Stop();
    }

    private void BackgroundSwitch(int vkCode)
    {
        SolidColorBrush brush = new SolidColorBrush(Colors.Red);
        for (int i = 0; i < _usedKeys.Count && i < _slots.Count; i++)
        {
            if (vkCode == _usedKeys[i]) _slots[i].Background = brush;
        }
    }

    private void BackgroundReset()
    {
        foreach (TextBlock l in _slots) l.Background = new SolidColorBrush(Color.FromRgb(161, 182, 200));
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        DragMove();
    }

    private static void SetHook()
    {
        IntPtr hInstance = LoadLibrary("User32");
        _hook = SetWindowsHookEx(13, _proc, hInstance, 0);
    }

    private static void UnHook()
    {
        UnhookWindowsHookEx(_hook);
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        SetHook();
    }

    protected override void OnClosed(EventArgs e)
    {
        Current.MainWindow?.Show();
        UnHook();
    }

    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
}