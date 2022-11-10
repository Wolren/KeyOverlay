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

public partial class KeyboardOverlay : Window
{
    private readonly DispatcherTimer _afk = new();
    private readonly DispatcherTimer _timer = new();
    private double _clicks, _total, _intervalClicks;
    private List<double> _cur = new();
    private static readonly List<Key> _keys = new() {
        Key.Escape, Key.F1, Key.F2, Key.F3, Key.F4, Key.F5, Key.F6,
        Key.F7, Key.F8, Key.F9, Key.F10, Key.F11, Key.F12,
        Key.Oem3, Key.D0, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6,
        Key.D7, Key.D8, Key.D9, Key.OemMinus, Key.OemPlus, Key.Back,
        Key.Tab, Key.Q, Key.W, Key.E, Key.R, Key.T, Key.Y, Key.U, Key.I,
        Key.O, Key.P, Key.OemOpenBrackets, Key.Oem6, Key.Oem5,
        Key.CapsLock, Key.A, Key.S, Key.D, Key.F, Key.G, Key.H, Key.J, Key.K,
        Key.L, Key.Oem1, Key.OemQuotes, Key.Return,
        Key.LeftShift, Key.Z, Key.X, Key.C, Key.V, Key.B, Key.N, Key.M,
        Key.OemComma, Key.OemPeriod, Key.OemQuestion, Key.RightShift,
        Key.LeftCtrl, Key.LWin, Key.LeftAlt, Key.Space,
        Key.RightAlt, Key.Apps, Key.RightCtrl
    };



    public KeyboardOverlay()
    {
        InitializeComponent();
        KeyDown += Window_KeyDown;
        _timer.Tick += TimerTick;
        _afk.Tick += EndTick;
        _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        _afk.Interval = new TimeSpan(0, 0, 2);
        Clicks.Text = "0";
        Cps.Text = "0";
        Cps.FontSize = 20;
        Clicks.FontSize = 20;
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        DragMove();
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if (!_keys.Contains(e.Key)) return;
        BackgroundSwitch(e);
        if (e.IsRepeat) return;
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
    
    private void BackgroundSwitch(KeyEventArgs e)
    {
        var brush = new SolidColorBrush(Colors.Red);
        if (key(e, Key.Escape)) Esc.Background = brush;
        else if (key(e, Key.F1)) F1.Background = brush;
        else if (key(e, Key.F2)) F2.Background = brush;
        else if (key(e, Key.F3)) F3.Background = brush;
        else if (key(e, Key.F4)) F4.Background = brush;
        else if (key(e, Key.F5)) F5.Background = brush;
        else if (key(e, Key.F6)) F6.Background = brush;
        else if (key(e, Key.F7)) F7.Background = brush;
        else if (key(e, Key.F8)) F8.Background = brush;
        else if (key(e, Key.F9)) F9.Background = brush;
        else if (key(e, Key.F10)) F10.Background = brush;
        else if (key(e, Key.F11)) F11.Background = brush;
        else if (key(e, Key.F12)) F12.Background = brush;


        else if (key(e, Key.Oem3)) N1.Background = brush;
        else if (key(e, Key.D1)) N2.Background = brush;
        else if (key(e, Key.D2)) N3.Background = brush;
        else if (key(e, Key.D3)) N4.Background = brush;
        else if (key(e, Key.D4)) N5.Background = brush;
        else if (key(e, Key.D5)) N6.Background = brush;
        else if (key(e, Key.D6)) N7.Background = brush;
        else if (key(e, Key.D7)) N8.Background = brush;
        else if (key(e, Key.D8)) N9.Background = brush;
        else if (key(e, Key.D9)) N10.Background = brush;
        else if (key(e, Key.D0)) N11.Background = brush;
        else if (key(e, Key.OemMinus)) N12.Background = brush;
        else if (key(e, Key.OemPlus)) N13.Background = brush;
        else if (key(e, Key.Back)) N14.Background = brush;
        
        
        else if(key(e, Key.Tab)) Tab.Background = brush;
        else if(key(e, Key.Q)) Q.Background = brush;
        else if(key(e, Key.W)) W.Background = brush;
        else if(key(e, Key.E)) E.Background = brush;
        else if(key(e, Key.R)) R.Background = brush;
        else if(key(e, Key.T)) T.Background = brush;
        else if(key(e, Key.Y)) Y.Background = brush;
        else if(key(e, Key.U)) U.Background = brush;
        else if(key(e, Key.I)) I.Background = brush;
        else if(key(e, Key.O)) O.Background = brush;
        else if(key(e, Key.P)) P.Background = brush;
        else if (key(e, Key.OemOpenBrackets)) P1.Background = brush;
        else if (key(e, Key.Oem6)) P2.Background = brush;
        else if (key(e, Key.Oem5)) P3.Background = brush;
        
        
        else if(key(e, Key.Capital)) A1.Background = brush;
        else if(key(e, Key.A)) A.Background = brush;
        else if(key(e, Key.S)) S.Background = brush;
        else if(key(e, Key.D)) D.Background = brush;
        else if(key(e, Key.F)) F.Background = brush;
        else if(key(e, Key.G)) G.Background = brush;
        else if(key(e, Key.H)) H.Background = brush;
        else if(key(e, Key.J)) J.Background = brush;
        else if(key(e, Key.K)) K.Background = brush;
        else if(key(e, Key.L)) L.Background = brush;
        else if(key(e, Key.Oem1)) A2.Background = brush;
        else if(key(e, Key.OemQuotes)) A3.Background = brush;
        else if(key(e, Key.Return)) A4.Background = brush;
        
        
        else if(key(e, Key.LeftShift)) Z1.Background = brush;
        else if(key(e, Key.Z)) Z.Background = brush;
        else if(key(e, Key.X)) X.Background = brush;
        else if(key(e, Key.C)) C.Background = brush;
        else if(key(e, Key.V)) V.Background = brush;
        else if(key(e, Key.B)) B.Background = brush;
        else if(key(e, Key.N)) N.Background = brush;
        else if(key(e, Key.M)) M.Background = brush;
        else if(key(e, Key.OemComma)) Z2.Background = brush;
        else if(key(e, Key.OemPeriod)) Z3.Background = brush;
        else if(key(e, Key.OemQuestion)) Z4.Background = brush;
        else if(key(e, Key.RightShift)) Z5.Background = brush;
        
        
        else if(key(e, Key.LeftCtrl)) L1.Background = brush;
        else if(key(e, Key.LWin)) L2.Background = brush;
        else if(key(e, Key.LeftAlt)) L3.Background = brush;
        else if(key(e, Key.Space)) L4.Background = brush;
        else if(key(e, Key.RightAlt)) L5.Background = brush;
        else if(key(e, Key.Apps)) L7.Background = brush;
        else if(key(e, Key.RightCtrl)) L8.Background = brush;
    }

    private static bool key(KeyEventArgs e, Key key)
    {
        return e.Key.Equals(key);
    }

    private void BackgroundReset()
    {
        var brush = new SolidColorBrush(Color.FromRgb(161, 182, 200));
        List<TextBlock> lines = new() { 
            Esc, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12,
            N1, N2, N3, N4, N5, N6, N7, N8, N9, N10, N11, N12, N13, N14,
            Tab, Q, W, E, R, T, Y, U, I, O, P, P1, P2, P3,
            A1, A, S, D, F, G, H, J, K, L, A2, A3, A4,
            Z1, Z, X, C, V, B, N, M, Z2, Z3, Z4, Z5,
            L1, L2, L3, L4, L5, L6, L7, L8
        };
        foreach (var l in lines) { l.Background = brush; }
    }
}