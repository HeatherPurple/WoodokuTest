using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] public int row;
    [SerializeField] public int column;
    [SerializeField] public CellStateEnum cellState; 

    private Image image;
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private Sprite fullSprite;

    public CellStateEnum CellState { get; private set; }

    private void Awake()
    {
        image = GetComponent<Image>();
        
    }

    private void Start()
    {
        if (cellState == CellStateEnum.full)
        {
            ChangeCellState();
        }
    }

    public void ChangeCellState()
    {
        if (CellState == CellStateEnum.empty)
        {
            CellState = CellStateEnum.full;
        }
        else
        {
            CellState = CellStateEnum.empty;
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
