using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private int cellCount = 81;

    [SerializeField] private List<Cell> cells = new();
    //[SerializeField] private Cell[,] cells1;

    private void Awake()
    {
        for (int i = 0; i < cellCount; i++)
        {
            cells.Add(transform.GetChild(i).GetComponent<Cell>());
            
        }
    }

    public void UpdateGrid(Cell cell)
    {
        CleanArea(cell);

    }

    private void CleanArea(Cell cell)
    {
        List<Cell> row = new List<Cell>();
        List<Cell> column = new List<Cell>();
        List<Cell> block = new List<Cell>();

        foreach (var c in cells)
        {
            if (c.row == cell.row)
            {
                row.Add(c);
            }
            if (c.column == cell.column)
            {
                column.Add(c);
            }
            if (c.block == cell.block)
            {
                block.Add(c);
            }
        }

        bool rowIsFull = true;
        bool columnIsFull = true;
        bool blockIsFull = true;

        foreach (var c in row)
        {
            if (c.CellState == CellStateEnum.empty)
            {
                rowIsFull = false;
                break;
            }
        }

        foreach (var c in column)
        {
            if (c.CellState == CellStateEnum.empty)
            {
                columnIsFull = false;
                break;
            }
        }

        foreach (var c in block)
        {
            if (c.CellState == CellStateEnum.empty)
            {
                blockIsFull = false;
                break;
            }
        }

        if (rowIsFull)
        {
            row.ForEach(c => c.ChangeCellState());
        }
        if (columnIsFull)
        {
            column.ForEach(c => c.ChangeCellState());
        }
        if (blockIsFull)
        {
            block.ForEach(c => c.ChangeCellState());
        }
    }

  




}
