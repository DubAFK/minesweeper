using System;

namespace Minesweeper.Models;

public class CellModel
{
    public bool IsMine { get; private set; } = false;
    public bool IsOpen { get; private set; } = false;
    public int MinesAround { get; private set; } = 0;

    // public string Content
    // {
    //     get
    //     {
    //         if(IsOpen)
    //         {
    //             if(IsMine) return "💣";
    //             else if(MinesAround > 0) return MinesAround.ToString();
    //             else return "";
    //         }
    //         else
    //         {
    //             return "";
    //         }
    //     }
    // }

    public void SetMine() => IsMine = true;
    public void OpenCell() => IsOpen = true;
    public void IncreaseNumberOfMinesAround() => MinesAround++;
}
