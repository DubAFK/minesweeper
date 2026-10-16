using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Minesweeper.Models;

public class FieldModel
{
    public FieldModel(int height, int width, int numberOfMines)
    {
        if(height <= 0 || height > 100) throw new ArgumentException("Incorrect height.");
        if(width <= 0 || width > 100) throw new ArgumentException("Incorrect width.");
        if(numberOfMines <= 0 || numberOfMines >= height * width) throw new ArgumentException("Incorrect amount of mines.");

        _fieldHeight = height;
        _fieldWidth = width;
        _numberOfMines = numberOfMines;
        _numberOfSafeCells = _fieldHeight * _fieldWidth - _numberOfMines;
        _numberOfOpenCells = 0;

        _field = new CellModel[_fieldHeight, _fieldWidth];
        for(int i = 0; i < _fieldHeight; i++)
        {
            for(int j = 0; j < _fieldWidth; j++)
            {
                _field[i, j] = new CellModel();
            }
        }

        SetMines(numberOfMines);
        CountNumbersOfMinesAround();
    }

    private int _fieldHeight;
    private int _fieldWidth;
    private int _numberOfMines;
    private int _numberOfSafeCells;
    private int _numberOfOpenCells;
    public string GameStatus { get; private set; } = "Just started. Good luck!";
    private CellModel[,] _field;

    public int Height => _fieldHeight;
    public int Width => _fieldWidth;
    
    public CellModel this[int row, int col] => _field[row, col];

    public void OpenCellWithCoords(int row, int col)
    {
        Console.WriteLine($"You open the cell with coords ({row},{col})");
        if(!_field[row, col].IsOpen)
        {
            _field[row, col].OpenCell();
            if(_field[row, col].IsMine)
            {
                GameStatus = "You lost! :(";
                return;
            }

            _numberOfOpenCells++;

            if(_field[row, col].MinesAround == 0)
            {
                OpenCellsAround(row, col);
            }

            if(CheckConditions())
            {
                return;
            }
        }
    }

    private void OpenCellsAround(int row, int col)
    {
        if(row > 0)
        {
            if(col > 0)
            {
                OpenCellWithCoords(row - 1, col - 1);
            }

            OpenCellWithCoords(row - 1, col);
            
            if(col < _fieldWidth - 1)
            {
                OpenCellWithCoords(row - 1, col + 1);
            }
        }

        if(col > 0)
        {
            OpenCellWithCoords(row, col - 1);
        }

        if(col < _fieldWidth - 1)
        {
            OpenCellWithCoords(row, col + 1);
        }

        if(row < _fieldHeight - 1)
        {
            if(col > 0)
            {
                OpenCellWithCoords(row + 1, col - 1);
            }

            OpenCellWithCoords(row + 1, col);

            if(col < _fieldWidth - 1)
            {
                OpenCellWithCoords(row + 1, col + 1);
            }
        }
    }

    private bool CheckConditions()
    {
        if(_numberOfOpenCells == _numberOfSafeCells)
        {
            GameStatus = "Victory! Congratulations :)";
            return true;
            // Debug.WriteLine("YOU WIN!");
            // Console.WriteLine("YOU WIN!");
            // GameWin();
        }
        else
        {
            GameStatus = $"In progress... There are {_numberOfSafeCells - _numberOfOpenCells} cells left to open.";
            return false;
        }
    }

    private void SetMines(int numberOfMines)
    {
        List<CellModel> freeCells = new List<CellModel>(_fieldHeight * _fieldWidth);

        for(int i = 0; i < _fieldHeight; i++)
        {
            for(int j = 0; j < _fieldWidth; j++)
            {
                freeCells.Add(_field[i, j]);
            }
        }

        Random random = new Random();
        int minesCounter = 0;

        while(minesCounter < numberOfMines)
        {
            int randomCellIndex = random.Next(freeCells.Count);
            freeCells[randomCellIndex].SetMine();
            freeCells.RemoveAt(randomCellIndex);
            minesCounter++;
        }
    }

    private void CountNumbersOfMinesAround()
    {
        for(int i = 0; i < _fieldHeight; i++)
        {
            for(int j = 0; j < _fieldWidth; j++)
            {
                if(i > 0)
                {
                    if(j > 0)
                    {
                        if(_field[i - 1, j - 1].IsMine)
                        {
                            _field[i, j].IncreaseNumberOfMinesAround();
                        }
                    }

                    if(_field[i - 1, j].IsMine)
                    {
                        _field[i, j].IncreaseNumberOfMinesAround();
                    }

                    if(j < _fieldWidth - 1)
                    {
                        if(_field[i - 1, j + 1].IsMine)
                        {
                            _field[i, j].IncreaseNumberOfMinesAround();
                        }
                    }
                }

                if(j > 0)
                {
                    if(_field[i, j - 1].IsMine)
                    {
                        _field[i, j].IncreaseNumberOfMinesAround();
                    }
                }

                if(j < _fieldWidth - 1)
                {
                    if(_field[i, j + 1].IsMine)
                    {
                        _field[i, j].IncreaseNumberOfMinesAround();
                    }
                }

                if(i < _fieldHeight - 1)
                {
                    if(j > 0)
                    {
                        if(_field[i + 1, j - 1].IsMine)
                        {
                            _field[i, j].IncreaseNumberOfMinesAround();
                        }
                    }

                    if(_field[i + 1, j].IsMine)
                    {
                        _field[i, j].IncreaseNumberOfMinesAround();
                    }

                    if(j < _fieldWidth - 1)
                    {
                        if(_field[i + 1, j + 1].IsMine)
                        {
                            _field[i, j].IncreaseNumberOfMinesAround();
                        }
                    }
                }
            }
        }
    }

}