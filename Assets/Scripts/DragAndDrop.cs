using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragAndDrop : MonoBehaviour, IDragHandler, IDropHandler
{
    private RectTransform draggingObjectRectTransform;

    [SerializeField]private GameObject currentCell;
    private GameObject currentPossibleCell;

    private void Awake()
    {
        draggingObjectRectTransform = transform as RectTransform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentPossibleCell = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentPossibleCell = null;
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
            ChangeCurrentCell();
        }
        
    }

    private void CheckIfCellIsFree(GameObject cell)
    {
        
    }

    private void ChangeCurrentCell()
    {
        if (currentPossibleCell != null)
        {
            currentCell = currentPossibleCell;
            currentPossibleCell = null;
        }
        draggingObjectRectTransform.position = currentCell.transform.position;
    }
}
