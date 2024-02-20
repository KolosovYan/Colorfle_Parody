using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private Cell[] cellsInLine;
    [SerializeField] private Indicator lineIndecator;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Sprite hasColorSprite;
    [SerializeField] private Sprite correctSprite;

    private List<Cell> emptyCells = new List<Cell>();

    private void Awake()
    {
        foreach (Cell c in cellsInLine)
        {
            c.SetInteractiveState(false);
            c.SetCanTouch(false);
            emptyCells.Add(c);
        }
    }

    public void SetIndicator(float percentage, Color clr)
    {
        lineIndecator.SetText(percentage);
        lineIndecator.SetColor(clr);
    }

    public void SetCellSprite(int cellIndex, bool correctPlace)
    {
        if (correctPlace)
            cellsInLine[cellIndex].SetFrameSprite(correctSprite);
        else
            cellsInLine[cellIndex].SetFrameSprite(hasColorSprite);
    }

    public bool HasEmptyCells()
    {
        return emptyCells.Count > 0;
    }

    public int GetLineLength()
    {
        return cellsInLine.Length;
    }

    public void SetCellColor(int index, Color clr)
    {
        cellsInLine[index].SetColor(clr);
        emptyCells.Remove(cellsInLine[index]);
    }

    public void EraseCellColor(int index)
    {
        cellsInLine[index].SetColor(defaultColor);
        
        if (!emptyCells.Contains(cellsInLine[index]))
            emptyCells.Add(cellsInLine[index]);
    }
}
