using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragAndDrop : MonoBehaviour, IDragHandler, IDropHandler
{
    private RectTransform draggingObjectRectTransform;

    [SerializeField]private GameObject currentPocket;

    private List<GameObject> currentCells = new List<GameObject>();

    private void Awake()
    {
        draggingObjectRectTransform = transform as RectTransform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Cell>().MarkCell(true);
        currentCells.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<Cell>().MarkCell(false);
        currentCells.Remove(collision.gameObject);
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
        if (FigureCanBePlaced())
        {
            foreach (var cell in currentCells)
            {
                cell.GetComponent<Cell>().ChangeCellState();
            }
            Destroy(gameObject);
        }
        draggingObjectRectTransform.position = currentPocket.transform.position;
    }

    private bool FigureCanBePlaced()
    {
        if (currentCells.Count <= 0)
        {
            return false;
        }
        if (currentCells.Count != transform.childCount)
        {
            return false;
        }

        foreach (var cell in currentCells)
        {
            if (cell.GetComponent<Cell>().CellState == CellStateEnum.full)
            {
                return false;
            }
        }
        return true;
    }
}
