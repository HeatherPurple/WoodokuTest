using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField]public int row;  
    [SerializeField]public int column; 
    [SerializeField]public CellStateEnum cellState; 

    private Image image;

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

    public void MarkCell(bool isMarked)
    {
        if (CellState == CellStateEnum.full)
        {
            return;
        }

        if (isMarked)
        {
            image.color = Color.yellow;
        }
        else
        {
            image.color = Color.white;
        }
    }

    private void UpdateCellImage()
    {
        if (CellState == CellStateEnum.empty)
        {
            image.color = Color.white;
        }
        else
        {
            image.color = Color.grey;
        }
    }


}

public enum CellStateEnum
{
    empty,
    full
}
