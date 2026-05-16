using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Programowanie.Overlays;
using static System.Windows.Application;
using static Programowanie.Properties.Settings;

namespace Programowanie.Views;

public partial class SingleKeyView
{
    private readonly List<string> _lines;

    [Localizable(false)]
    public SingleKeyView()
    {
        InitializeComponent();
        _lines = new List<string>
        {
            "Esc", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12",
            "` ~", "1 !", "2 @", "3 #", "4 $", "5 %", "6 ^", "7 &", "8 *", "9 (", "0 )", "- _", "= +", "BACKSPACE",
            "TAB", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "[ {", "] }", "\\ |",
            "CAPS LOCK", "A", "S", "D", "F", "G", "H", "J", "K", "L", "; :", "' \"", "ENTER",
            "SHIFT", "Z", "X", "C", "V", "B", "N", "M", ", <", ". >", "/ ?", "SHIFT",
            "CTRL", "WIN", "ALT", "SPACE", "ALT", "FN", "APPS", "CTRL"
        };
        List<ComboBox> blocks = new List<ComboBox>
        {
            Slot1, Slot2, Slot3, Slot4, Slot5
        };
        List<string> settings = new List<string>
        {
            Default.Slot1, Default.Slot2, Default.Slot3, Default.Slot4, Default.Slot5
        };
        for (int i = 0; i < blocks.Count; i++)
        {
            blocks[i].SelectedValue = settings[i];
            blocks[i].SelectionChanged += slotChanged;
        }

        foreach (ComboBox comboBox in blocks)
        {
            foreach (string s in _lines)
            {
                comboBox.Items.Add(s);
            }
        }
    }

    private void slotChanged(object sender, SelectionChangedEventArgs e)
    {
        if (Slot1.SelectedItem != null)
        {
            string selectedItem1 = Slot1.SelectedItem.ToString() ?? string.Empty;
            Default.Slot1 = selectedItem1;
            Default.Slot1Key = Array.IndexOf(_lines.ToArray(), selectedItem1);
        }

        if (Slot2.SelectedItem != null)
        {
            string selectedItem2 = Slot2.SelectedItem.ToString() ?? string.Empty;
            Default.Slot2 = selectedItem2;
            Default.Slot2Key = Array.IndexOf(_lines.ToArray(), selectedItem2);
        }

        if (Slot3.SelectedItem != null)
        {
            string selectedItem3 = Slot3.SelectedItem.ToString() ?? string.Empty;
            Default.Slot3 = selectedItem3;
            Default.Slot3Key = Array.IndexOf(_lines.ToArray(), selectedItem3);
        }

        if (Slot4.SelectedItem != null)
        {
            string selectedItem4 = Slot4.SelectedItem.ToString() ?? string.Empty;
            Default.Slot4 = selectedItem4;
            Default.Slot4Key = Array.IndexOf(_lines.ToArray(), selectedItem4);
        }

        if (Slot5.SelectedItem != null)
        {
            string selectedItem5 = Slot5.SelectedItem.ToString() ?? string.Empty;
            Default.Slot5 = selectedItem5;
            Default.Slot5Key = Array.IndexOf(_lines.ToArray(), selectedItem5);
            Default.Save();
        }
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (KeyboardView.isWindowOpen<SingleKeyOverlay>()) return;
        SingleKeyOverlay mouseOverlay = new SingleKeyOverlay();
        mouseOverlay.Show();
        if (Default.Close && Current.MainWindow != null)
        {
            Current.MainWindow.Hide();
        }
    }
}