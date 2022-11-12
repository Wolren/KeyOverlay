using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Programowanie.Overlays;

public partial class KeyboardOverlay
{
    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hInstance, uint threadId);

    [DllImport("user32.dll")]
    private static extern bool UnhookWindowsHookEx(IntPtr hInstance);

    [DllImport("user32.dll")]
    private static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    private static extern IntPtr LoadLibrary(string lpFileName);
    
    private static readonly List<int> Keys = new()
    {
        0x1B, 0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79, 0x7A, 0x7B,
        0xC0, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30, 0xBD, 0xBB, 0x08,
        0x09, 0x51, 0x57, 0x45, 0x52, 0x54, 0x59, 0x55, 0x49, 0x4F, 0x50, 0xDB, 0xDD, 0xDC,
        0x14, 0x41, 0x53, 0x44, 0x46, 0x47, 0x48, 0x4A, 0x4B, 0x4C, 0xBA, 0xDE, 0x0D,
        0xA0, 0x5A, 0x58, 0x43, 0x56, 0x42, 0x4E, 0x4D, 0xBC, 0xBE, 0xBF, 0xA1,
        0xA2, 0x5B, 0xA4, 0x20, 0xA5, 0x5D, 0x11, 0xA3
    };
    
    public delegate void KeyEventHandler(int vkCode);
    public static event KeyEventHandler? OnKeyPressed;
    
    private const int WM_KEYDOWN = 0x100;
    private const int WM_KEYUP = 0x101;

    private static readonly List<int> KeysDown = new();
    private static readonly LowLevelKeyboardProc _proc = HookProc;
    private static IntPtr hhook = IntPtr.Zero;
    private readonly DispatcherTimer _afk = new();
    private readonly DispatcherTimer _timer = new();
    private double _clicks, _total, _intervalClicks;
    private List<double> _cur = new();

    public KeyboardOverlay()
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
        OnKeyPressed += KeyHandler;
    }
    
    private static IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam)
    {
        if (code < 0) return CallNextHookEx(hhook, code, (int)wParam, lParam);
        int vkCode = Marshal.ReadInt32(lParam);
        if (wParam == (IntPtr)WM_KEYDOWN)
        {
            if (!KeysDown.Contains(vkCode) && Keys.Contains(vkCode)) OnKeyPressed?.Invoke(vkCode);
            KeysDown.Add(vkCode);
        } else if (wParam == (IntPtr)WM_KEYUP)
        {
            KeysDown.RemoveAll(k => k == vkCode);
        }

        return CallNextHookEx(hhook, code, (int)wParam, lParam);
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

    private void BackgroundSwitch(int vkCode)
    {
        SolidColorBrush brush = new SolidColorBrush(Colors.Red);
        if (vkCode == 0x1B) Esc.Background = brush;
        else if (vkCode == 0x70) F1.Background = brush;
        else if (vkCode == 0x71) F2.Background = brush;
        else if (vkCode == 0x72) F3.Background = brush;
        else if (vkCode == 0x73) F4.Background = brush;
        else if (vkCode == 0x74) F5.Background = brush;
        else if (vkCode == 0x75) F6.Background = brush;
        else if (vkCode == 0x76) F7.Background = brush;
        else if (vkCode == 0x77) F8.Background = brush;
        else if (vkCode == 0x78) F9.Background = brush;
        else if (vkCode == 0x79) F10.Background = brush;
        else if (vkCode == 0x7A) F11.Background = brush;
        else if (vkCode == 0x7B) F12.Background = brush;


        else if (vkCode == 0xC0) N1.Background = brush;
        else if (vkCode == 0x31) N2.Background = brush;
        else if (vkCode == 0x32) N3.Background = brush;
        else if (vkCode == 0x33) N4.Background = brush;
        else if (vkCode == 0x34) N5.Background = brush;
        else if (vkCode == 0x35) N6.Background = brush;
        else if (vkCode == 0x36) N7.Background = brush;
        else if (vkCode == 0x37) N8.Background = brush;
        else if (vkCode == 0x38) N9.Background = brush;
        else if (vkCode == 0x39) N10.Background = brush;
        else if (vkCode == 0x30) N11.Background = brush;
        else if (vkCode == 0xBD) N12.Background = brush;
        else if (vkCode == 0xBB) N13.Background = brush;
        else if (vkCode == 0x08) N14.Background = brush;


        else if (vkCode == 0x09) Tab.Background = brush;
        else if (vkCode == 0x51) Q.Background = brush;
        else if (vkCode == 0x57) W.Background = brush;
        else if (vkCode == 0x45) E.Background = brush;
        else if (vkCode == 0x52) R.Background = brush;
        else if (vkCode == 0x54) T.Background = brush;
        else if (vkCode == 0x59) Y.Background = brush;
        else if (vkCode == 0x55) U.Background = brush;
        else if (vkCode == 0x49) I.Background = brush;
        else if (vkCode == 0x4F) O.Background = brush;
        else if (vkCode == 0x50) P.Background = brush;
        else if (vkCode == 0xDB) P1.Background = brush;
        else if (vkCode == 0xDD) P2.Background = brush;
        else if (vkCode == 0xDC) P3.Background = brush;


        else if (vkCode == 0x14) A1.Background = brush;
        else if (vkCode == 0x41) A.Background = brush;
        else if (vkCode == 0x53) S.Background = brush;
        else if (vkCode == 0x44) D.Background = brush;
        else if (vkCode == 0x46) F.Background = brush;
        else if (vkCode == 0x47) G.Background = brush;
        else if (vkCode == 0x48) H.Background = brush;
        else if (vkCode == 0x4A) J.Background = brush;
        else if (vkCode == 0x4B) K.Background = brush;
        else if (vkCode == 0x4C) L.Background = brush;
        else if (vkCode == 0xBA) A2.Background = brush;
        else if (vkCode == 0xDE) A3.Background = brush;
        else if (vkCode == 0x0D) A4.Background = brush;


        else if (vkCode == 0xA0) Z1.Background = brush;
        else if (vkCode == 0x5A) Z.Background = brush;
        else if (vkCode == 0x58) X.Background = brush;
        else if (vkCode == 0x43) C.Background = brush;
        else if (vkCode == 0x56) V.Background = brush;
        else if (vkCode == 0x42) B.Background = brush;
        else if (vkCode == 0x4E) N.Background = brush;
        else if (vkCode == 0x4D) M.Background = brush;
        else if (vkCode == 0xBC) Z2.Background = brush;
        else if (vkCode == 0xBE) Z3.Background = brush;
        else if (vkCode == 0xBF) Z4.Background = brush;
        else if (vkCode == 0xA1) Z5.Background = brush;


        else if (vkCode == 0xA2) L1.Background = brush;
        else if (vkCode == 0x5B) L2.Background = brush;
        else if (vkCode == 0xA4) L3.Background = brush;
        else if (vkCode == 0x20) L4.Background = brush;
        else if (vkCode == 0xA5) L5.Background = brush;
        else if (vkCode == 0x5D) L7.Background = brush;
        else if (vkCode == 0xA3) L8.Background = brush;
    }

    private void BackgroundReset()
    {
        SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(161, 182, 200));
        List<TextBlock> lines = new()
        {
            Esc, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12,
            N1, N2, N3, N4, N5, N6, N7, N8, N9, N10, N11, N12, N13, N14,
            Tab, Q, W, E, R, T, Y, U, I, O, P, P1, P2, P3,
            A1, A, S, D, F, G, H, J, K, L, A2, A3, A4,
            Z1, Z, X, C, V, B, N, M, Z2, Z3, Z4, Z5,
            L1, L2, L3, L4, L5, L6, L7, L8
        };
        foreach (TextBlock l in lines) l.Background = brush;
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        DragMove();
    }

    private static void SetHook()
    {
        IntPtr hInstance = LoadLibrary("User32");
        hhook = SetWindowsHookEx(13, _proc, hInstance, 0);
    }

    private static void UnHook()
    {
        UnhookWindowsHookEx(hhook);
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        SetHook();
    }

    protected override void OnClosed(EventArgs e)
    {
        UnHook();
    }

    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
}