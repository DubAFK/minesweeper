using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Minesweeper.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _heightString = "20";

    [ObservableProperty]
    private string _widthString = "20";

    [ObservableProperty]
    private string _minesCountString = "40";

    private int _height = 20;

    private int _width = 20;

    private int _minesCount = 40;

    public event Action<int, int, int>? GameStarted;

    [RelayCommand]
    private void StartGame()
    {
        int.TryParse(HeightString, out _height);
        int.TryParse(WidthString, out _width);
        int.TryParse(MinesCountString, out _minesCount);

        int maxMines = (_width * _height) - 1;
        if (_minesCount > maxMines)
        {
            _minesCount = maxMines;
        }

        GameStarted?.Invoke(_width, _height, _minesCount);
    }
}
