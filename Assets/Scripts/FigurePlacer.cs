using System.Collections.Generic;
using UnityEngine;

public class FigurePlacer : MonoBehaviour
{
    [SerializeField] private Pocket currentPocket;
    [SerializeField] private Cell targetCell;
    [SerializeField] private Pocket targetPocket;
    [SerializeField] private BoolSubArray[] figureCells;
    [SerializeField] private GridController gridController;

    private RectTransform draggingObjectRectTransform;

    #region MONO
    private void Awake()
    {
        draggingObjectRectTransform = transform as RectTransform;
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Cell cell))
        {
            targetCell = cell;
            return;
        }

        if (collision.TryGetComponent(out Pocket pocket))
        {
            targetPocket = pocket;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Cell _))
        {
            targetCell = null;
            return;
        }

        if (collision.TryGetComponent(out Pocket _))
        {
            targetPocket = null;
        }
    }

    private bool FigureCanBePlaced(out List<Cell> list)
    {
        list = new List<Cell>();
        for (int i = 0; i < figureCells.Length; i++)
        {
            for (int j = 0; j < figureCells[i].subArray.Length; j++)
            {
                if (figureCells[i].subArray[j] == CellStateEnum.full)
                {
                    if (targetCell.row - 1 + i >= gridController.gridCells.Length ||
                        targetCell.column - 1 + j >= gridController.gridCells[i].subArray.Length)
                    {
                        return false;
                    }

                    Cell currentCell = gridController.gridCells[targetCell.row - 1 + i]
                        .subArray[targetCell.column - 1 + j];
                    if (currentCell.CellState == CellStateEnum.full)
                    {
                        return false;
                    }
                    list.Add(currentCell);
                }
            }
        }
        return true;
    }

    public void PlaceFigure()
    {
        if (targetPocket != null)
        {
            currentPocket = targetPocket;
            draggingObjectRectTransform.position = currentPocket.transform.position;
            return;
        }

        if (targetCell == null)
        {
            draggingObjectRectTransform.position = currentPocket.transform.position;
            return;
        }

        List<Cell> list;
        if (FigureCanBePlaced(out list))
        {
            list.ForEach(c => c.ChangeCellState(CellStateEnum.full));
            gridController.UpdateGrid(list);

            MainMenu.instance.DecreaseMoves();

            Destroy(gameObject);
        }
        draggingObjectRectTransform.position = currentPocket.transform.position;
    }
}
