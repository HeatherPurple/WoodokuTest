using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField]public int id; //change 
    [SerializeField]public int row;  
    [SerializeField]public int column; 
    [SerializeField]public int block;

    private Image image;
    private GridController gridController;

    public CellStateEnum CellState { get; private set; }

    private void Awake()
    {
        image = GetComponent<Image>();
        gridController = transform.parent.GetComponent<GridController>();
    }

    public void ChangeCellState()
    {
        if (CellState == CellStateEnum.empty)
        {
            CellState = CellStateEnum.full;
            gridController.UpdateGrid(this);
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
