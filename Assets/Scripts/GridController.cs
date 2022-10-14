using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CellSubArray
{
    public Cell[] subArray;
    public CellSubArray(int arraySize)
    {
        subArray = new Cell[arraySize];
    }

}

public class GridController : MonoBehaviour
{
    [SerializeField] public CellSubArray[] cells1;

    private void Awake()
    {
        int arraySize = (int)Mathf.Sqrt(transform.childCount);
        
        cells1 = new CellSubArray[arraySize];
        for (int i = 0; i < cells1.Length; i++)
        {
            cells1[i] = new CellSubArray(arraySize);
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Cell cell = transform.GetChild(i).GetComponent<Cell>();
            cells1[cell.row - 1].subArray[cell.column - 1] = cell;
        }
    }

    public void UpdateGrid(List<Cell> newCells)
    {
        HashSet<int> uniqueRows = GetUniqueRows(newCells);
        //HashSet<int> uniqueColumns = GetUniqueColumns(newCells);

        List<List<Cell>> cellsToClean1 = GetCellsFromRows(uniqueRows);
        //List<List<Cell>> cellsToClean2 = GetCellsFromColumns(uniqueColumns);

        for (int i = 0; i < cellsToClean1.Count; i++)
        {
            MainMenu.instance.score += 1;
        }
        cellsToClean1.ForEach(l => l.ForEach(c => c.ChangeCellState()));
        //cellsToClean2.ForEach(l => l.ForEach(c => c.ChangeCellState()));
    }

    private HashSet<int> GetUniqueRows(List<Cell> cells)
    {
        HashSet<int> rows = new HashSet<int>();

        foreach (var cell in cells)
        {
            rows.Add(cell.row);
        }

        return rows;
    }

    //private HashSet<int> GetUniqueColumns(List<Cell> cells)
    //{
    //    HashSet<int> columns = new HashSet<int>();

    //    foreach (var cell in cells)
    //    {
    //        columns.Add(cell.column);
    //    }

    //    return columns;
    //}

    private List<List<Cell>> GetCellsFromRows(HashSet<int> rows)
    {
        List<List<Cell>> cellsToClean = new List<List<Cell>>();

        foreach (var rowNumber in rows)
        {
            List<Cell> newRow = new List<Cell>();
            bool newRowIsFull = true;

            for (int i = 0; i < cells1[rowNumber - 1].subArray.Length; i++)
            {
                if (cells1[rowNumber - 1].subArray[i].CellState == CellStateEnum.empty)
                {
                    newRowIsFull = false;
                    break;
                }
                newRow.Add(cells1[rowNumber - 1].subArray[i]);
            }
            if (newRowIsFull)
            {
                cellsToClean.Add(newRow);
            }
        }
        return cellsToClean;
    }

    //private List<List<Cell>> GetCellsFromColumns(HashSet<int> columns)
    //{
    //    List<List<Cell>> cellsToClean = new List<List<Cell>>();

    //    foreach (var columnNumber in columns)
    //    {
    //        List<Cell> newColumn = new List<Cell>();
    //        bool newColumnIsFull = true;

    //        for (int i = 0; i < cells1.Length; i++)
    //        {
    //            if (cells1[i].subArray[columnNumber - 1].CellState == CellStateEnum.empty)
    //            {
    //                newColumnIsFull = false;
    //                break;
    //            }
    //            newColumn.Add(cells1[i].subArray[columnNumber - 1]);
    //        }
    //        if (newColumnIsFull)
    //        {
    //            cellsToClean.Add(newColumn);
    //        }
    //    }
    //    return cellsToClean;
    //}



}
