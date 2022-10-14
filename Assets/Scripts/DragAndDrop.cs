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
    [SerializeField]private GameObject currentPocket;

    [SerializeField] private Cell targetCell;
    [SerializeField] public BoolSubArray[] cells;

    [SerializeField] private GridController gridController;

    private void Awake()
    {
        draggingObjectRectTransform = transform as RectTransform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        targetCell = collision.GetComponent<Cell>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        targetCell = null;
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
        if (targetCell == null)
        {
            return;
        }

        List<Cell> list;
        if (FigureCanBePlaced(out list))
        {
            list.ForEach(c => c.ChangeCellState());
            gridController.UpdateGrid(list);

            Destroy(gameObject);
        }
        draggingObjectRectTransform.position = currentPocket.transform.position;
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
