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
    [SerializeField] public CellSubArray[] gridCells;

    #region MONO
    private void Awake()
    {
        int arraySize = (int)Mathf.Sqrt(transform.childCount);

        gridCells = new CellSubArray[arraySize];
        for (int i = 0; i < gridCells.Length; i++)
        {
            gridCells[i] = new CellSubArray(arraySize);
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Cell cell = transform.GetChild(i).GetComponent<Cell>();
            gridCells[cell.row - 1].subArray[cell.column - 1] = cell;
        }
    }
    #endregion


    public void UpdateGrid(List<Cell> newCells)
    {
        HashSet<int> uniqueRows = GetUniqueRows(newCells);

        List<List<Cell>> cellsToClean1 = GetCellsFromRows(uniqueRows);

        for (int i = 0; i < cellsToClean1.Count; i++)
        {
            MainMenu.instance.Score += 1;
        }
        cellsToClean1.ForEach(l => l.ForEach(c => c.ChangeCellState(CellStateEnum.empty)));
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

    private List<List<Cell>> GetCellsFromRows(HashSet<int> rows)
    {
        List<List<Cell>> cellsToClean = new List<List<Cell>>();

        foreach (var rowNumber in rows)
        {
            List<Cell> newRow = new List<Cell>();
            bool newRowIsFull = true;

            for (int i = 0; i < gridCells[rowNumber - 1].subArray.Length; i++)
            {
                if (gridCells[rowNumber - 1].subArray[i].CellState == CellStateEnum.empty)
                {
                    newRowIsFull = false;
                    break;
                }
                newRow.Add(gridCells[rowNumber - 1].subArray[i]);
            }
            if (newRowIsFull)
            {
                cellsToClean.Add(newRow);
            }
        }
        return cellsToClean;
    }
}
