using System;
using System.Drawing;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Minesweeper.Models;

namespace Minesweeper.ViewModels;

public partial class CellViewModel : ObservableObject
{
    private readonly CellModel _model;

    public int Row { get; private set; }
    public int Column { get; private set; }

    public CellViewModel(CellModel model, int row, int col)
    {
        _model = model;
        Row = row;
        Column = col;
    }

    public bool IsMine => _model.IsMine;
    public int MinesAround => _model.MinesAround;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayText))]
    [NotifyPropertyChangedFor(nameof(DisplayColor))]
    [NotifyPropertyChangedFor(nameof(TextColor))]
    private bool _isOpen;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayText))]
    [NotifyPropertyChangedFor(nameof(DisplayColor))]
    [NotifyPropertyChangedFor(nameof(TextColor))]
    private bool _isFlagged;

    public void OpenCell()
    {
        _model.OpenCell();
        IsOpen = _model.IsOpen;
    }
    public void RefreshState()
    {
        IsOpen = _model.IsOpen;
    }

    public string DisplayText
    {
        get
        {
            if(IsFlagged) return "F";
            if(!IsOpen) return "";
            if(IsMine) return "M";
            return MinesAround > 0 ? MinesAround.ToString() : "";
        }
    }

    public string TextColor
    {
        get
        {
            if(IsFlagged) return "RosyBrown";
            if(!IsOpen) return "Darkgray";
            if(IsMine) return "Black";

            if(MinesAround == 1) return "BurlyWood";
            if(MinesAround == 2) return "LightBlue";
            if(MinesAround == 3) return "GreenYellow";
            if(MinesAround == 4) return "CornflowerBlue";
            if(MinesAround == 5) return "Purple";
            if(MinesAround == 6) return "Goldenrod";
            if(MinesAround == 7) return "OrangeRed";
            if(MinesAround == 8) return "Maroon";

            else return "Gray";
        }
    }

    public string DisplayColor
    {
        get
        {
            if(!IsOpen) return "DimGray";
            if(IsMine) return "Crimson";
            if(IsOpen) return "Gray";

            else return "Gray";
        }
    }

}
