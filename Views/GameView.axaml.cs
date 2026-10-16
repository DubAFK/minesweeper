using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Minesweeper.ViewModels;

namespace Minesweeper.Views;

public partial class GameView : UserControl
{
    public GameView()
    {
        InitializeComponent();
    }

    private void OnCellPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border { DataContext: CellViewModel cellVm } && DataContext is GameViewModel gameVm)
        {
            var point = e.GetCurrentPoint(sender as Control);

            if (point.Properties.IsLeftButtonPressed)
            {
                gameVm.OpenCell(cellVm);
            }
            else if (point.Properties.IsRightButtonPressed)
            {
                gameVm.ToggleFlag(cellVm);
            }
        }
    }
}