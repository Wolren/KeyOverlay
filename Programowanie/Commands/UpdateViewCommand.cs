using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Programowanie.ViewModels;

namespace Programowanie.Commands;

public class UpdateViewCommand : ICommand
{
    private readonly MainViewModel _viewModel;

    public UpdateViewCommand(MainViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    [Localizable(false)]
    public void Execute(object? parameter)
    {
        Debug.Assert(parameter != null, nameof(parameter) + " != null");
        _viewModel.SelectedViewModel = parameter.ToString() switch
        {
            "Keyboard" => new KeyboardViewModel(),
            "Settings" => new SettingsViewModel(),
            "Mouse" => new MouseViewModel(),
            "Key" => new SingleKeyViewModel(),
            _ => _viewModel.SelectedViewModel
        };
    }
}