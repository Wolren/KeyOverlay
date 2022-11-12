using System;
using System.Diagnostics;
using System.Windows.Input;
using Programowanie.Overlays;
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

    public void Execute(object? parameter)
    {
        Debug.Assert(parameter != null, nameof(parameter) + " != null");
        //Można zastąpić switchem 
        if (parameter.ToString() == "Keyboard")
            _viewModel.SelectedViewModel = new KeyboardViewModel();
        else if (parameter.ToString() == "Settings")
            _viewModel.SelectedViewModel = new SettingsViewModel();
        else if (parameter.ToString() == "Mouse")
            _viewModel.SelectedViewModel = new MouseViewModel();
        else if (parameter.ToString() == "Key") _viewModel.SelectedViewModel = new SingleKeyViewModel();
    }
}