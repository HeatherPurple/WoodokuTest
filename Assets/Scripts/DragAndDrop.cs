using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class BoolSubArray
{
    public CellStateEnum[] subArray;
}

public class DragAndDrop : MonoBehaviour, IDragHandler, IDropHandler
{
    private RectTransform draggingObjectRectTransform;
    [SerializeField]private Pocket currentPocket;

    [SerializeField] private Cell targetCell;
    [SerializeField] private Pocket targetPocket;

    [SerializeField] public BoolSubArray[] cells;

    [SerializeField] private GridController gridController;

    private static int movesLeft = 1;


    private void Awake()
    {
        draggingObjectRectTransform = transform as RectTransform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Cell cell = new Cell();
        if (collision.TryGetComponent(out cell))
        {
            targetCell = cell;
            return;
        }

        Pocket pocket = new Pocket();
        if (collision.TryGetComponent(out pocket))
        {
            targetPocket = pocket;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Cell cell = new Cell();
        if (collision.TryGetComponent(out cell))
        {
            targetCell = null;
            return;
        }

        Pocket pocket = new Pocket();
        if (collision.TryGetComponent(out pocket))
        {
            targetPocket = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingObjectRectTransform,
            eventData.position,eventData.pressEventCamera, out var globalMousePosition))
        {
            draggingObjectRectTransform.position = globalMousePosition;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            PlaceFigure();
        }
    }

    

    private void PlaceFigure()
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
            list.ForEach(c => c.ChangeCellState());
            gridController.UpdateGrid(list);

            NextTurn();

            Destroy(gameObject);
        }
        draggingObjectRectTransform.position = currentPocket.transform.position;
    }

    private void NextTurn()
    {
        movesLeft -= 1;

        if (movesLeft <= 0)
        {
            string text;
            if (MainMenu.instance.score > 0)
            {
                text = "онаедю";
            }
            else
            {
                text = "ньхайю";
            }
            MainMenu.instance.Retry(text);
        }
    }

    private bool FigureCanBePlaced(out List<Cell> list)
    {
        list = new List<Cell>();
        for (int i = 0; i < cells.Length; i++)
        {
            for (int j = 0; j < cells[i].subArray.Length; j++)
            {
                if (cells[i].subArray[j] == CellStateEnum.full)
                {
                    if (targetCell.row - 1 + i >= gridController.cells1.Length ||
                        targetCell.column - 1 + j >= gridController.cells1[i].subArray.Length)
                    {
                        return false;
                    }

                    Cell currentCell = gridController.cells1[targetCell.row - 1 + i]
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
}
