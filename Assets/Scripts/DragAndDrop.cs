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

    private FigurePlacer figurePlacer;

    #region MONO
    private void Awake()
    {
        draggingObjectRectTransform = transform as RectTransform;
        figurePlacer = GetComponent<FigurePlacer>();
    }
    #endregion


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
            figurePlacer.PlaceFigure();
        }
    }
}
