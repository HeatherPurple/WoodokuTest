using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private CellStateEnum cellState; 
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private Sprite fullSprite;

    private Image image;

    public int row;
    public int column;

    public CellStateEnum CellState { get; private set; }

    #region MONO
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        ChangeCellState(cellState);
    }
    #endregion

    public void ChangeCellState(CellStateEnum state)
    {
        if (state == CellStateEnum.empty)
        {
            CellState = CellStateEnum.empty;
        }
        else
        {
            CellState = CellStateEnum.full;
        }
        UpdateCellImage();
    }

    private void UpdateCellImage()
    {
        if (CellState == CellStateEnum.empty)
        {
            image.sprite = emptySprite;
        }
        else
        {
            image.sprite = fullSprite;
        }
    }


}

public enum CellStateEnum
{
    empty,
    full
}
