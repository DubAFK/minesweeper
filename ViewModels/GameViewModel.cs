using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Minesweeper.Models;

namespace Minesweeper.ViewModels;

public partial class GameViewModel : ViewModelBase
{
    private readonly FieldModel _fieldModel;
    private readonly CellViewModel[,] _fieldViewModel;

    public ObservableCollection<CellViewModel> Cells { get; } = new();

    public int Height => _fieldModel.Height;
    public int Width => _fieldModel.Width;

    [ObservableProperty]
    private string _gameStatus;

    [ObservableProperty]
    private bool _isGameOver = false;

    public GameViewModel(int height, int width, int numberOfMines)
    {
        _fieldModel = new FieldModel(height, width, numberOfMines);
        _fieldViewModel = new CellViewModel[_fieldModel.Height, _fieldModel.Width];

        for(int i = 0; i < _fieldModel.Height; i++)
        {
            for(int j = 0; j < _fieldModel.Width; j++)
            {
                CellViewModel cellVm = new CellViewModel(_fieldModel[i, j], i, j);
                _fieldViewModel[i, j] = cellVm;
                Cells.Add(cellVm);
            }
        }

        _gameStatus = _fieldModel.GameStatus;
        // RevealAllMines();
        // RevealField();
    }

    public void OpenCell(CellViewModel cellVm)
    {
        if(IsGameOver || cellVm.IsFlagged || cellVm.IsOpen) return;
        Console.WriteLine($"You open the cell with coords ({cellVm.Row},{cellVm.Column}) from GameViewModel!");
        _fieldModel.OpenCellWithCoords(cellVm.Row, cellVm.Column);
        SyncWithModel();
    }

    public void ToggleFlag(CellViewModel cellVm)
    {
        if(IsGameOver || cellVm.IsOpen) return;
        Console.WriteLine($"You toggle flag on the cell with coords ({cellVm.Row},{cellVm.Column}) from GameViewModel!");
        cellVm.IsFlagged = !cellVm.IsFlagged;
    }

    private void SyncWithModel()
    {
        for(int i = 0; i < _fieldModel.Height; i++)
        {
            for(int j = 0; j < _fieldModel.Width; j++)
            {
                _fieldViewModel[i, j].RefreshState();
            }
        }

        GameStatus = _fieldModel.GameStatus;

        if(GameStatus == "You lost! :(" || GameStatus == "Victory! Congratulations :)")
        {
            IsGameOver = true;

            if(GameStatus == "You lost! :(")
            {
                RevealAllMines();
            }
        }
    }

    private void RevealAllMines()
    {
        for(int i = 0; i < _fieldModel.Height; i++)
        {
            for(int j = 0; j < _fieldModel.Width; j++)
            {
                if(_fieldModel[i, j].IsMine)
                {
                    _fieldModel[i, j].OpenCell();
                    _fieldViewModel[i, j].RefreshState();
                }
            }
        }
    }

    private void RevealField()
    {
        for(int i = 0; i < _fieldModel.Height; i++)
        {
            for(int j = 0; j < _fieldModel.Width; j++)
            {
                _fieldModel[i, j].OpenCell();
                _fieldViewModel[i, j].RefreshState();
            }
        }
    }
}
