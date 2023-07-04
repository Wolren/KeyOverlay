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
    private readonly List<ComboBox> _blocks;
    private static List<string>? _lines;

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
        _blocks = new List<ComboBox> { Slot1, Slot2, Slot3, Slot4, Slot5 };
        List<string> settings = new() { Default.Slot1, Default.Slot2, Default.Slot3, Default.Slot4, Default.Slot5 };
        for (int index = 0; index < _blocks.Count; index++)
        {
            ComboBox block = _blocks[index];
            block.SelectedValue = settings[index];
            block.SelectionChanged += SlotChanged;
            foreach (string str in _lines) block.Items.Add(str);
        }
    }

    private void SlotChanged(object sender, EventArgs e)
    {
        if (sender is not ComboBox block) return;
        int index = _blocks.IndexOf(block) + 1;
        string? selectedItem = block.SelectedValue.ToString();
        SaveSlotValue(index, selectedItem);
    }

    private static void SaveSlotValue(int index, string? selectedItem)
    {
        Default[$"Slot{index}"] = selectedItem;
        Default[$"Slot{index}Key"] = Array.IndexOf(_lines?.ToArray() ?? Array.Empty<string>(), selectedItem);
    }
    
    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (KeyboardView.IsWindowOpen<SingleKeyOverlay>()) return;
        SingleKeyOverlay singleKeyOverlay = new SingleKeyOverlay();
        singleKeyOverlay.Show();
        if (Default.Close && Current.MainWindow != null) Current.MainWindow.Hide();
    }
}
