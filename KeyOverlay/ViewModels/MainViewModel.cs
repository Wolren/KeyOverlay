﻿using System.Windows.Input;
using Programowanie.Commands;

namespace Programowanie.ViewModels;

public class MainViewModel : BaseViewModel
{
    private BaseViewModel? _selectedViewModel;

    public MainViewModel()
    {
        UpdateViewCommand = new UpdateViewCommand(this);
    }

    public BaseViewModel? SelectedViewModel
    {
        get => _selectedViewModel;
        set
        {
            _selectedViewModel = value;
            OnPropertyChanged(nameof(SelectedViewModel));
        }
    }

    public ICommand UpdateViewCommand { get; set; }
}