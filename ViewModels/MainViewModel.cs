using System;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Minesweeper.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableObject _currentViewModel;

    public MainViewModel()
    {
        var settingsVm = new SettingsViewModel();

        settingsVm.GameStarted += (width, height, mines) =>
        {
            CurrentViewModel = new GameViewModel(width, height, mines);
            Console.WriteLine("Game has started!");
        };

        CurrentViewModel = settingsVm;
    }

}