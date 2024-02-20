using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game_Controller : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private Game_UI_Controller game_UI_Controller;
    [SerializeField] private Circle currentCircle;
    [SerializeField] private Lines_Manager lineManager;

    public static Game_Controller instance;

    [Header("Available colors")]
    [SerializeField] private List<Color> colors;

    public bool InGame { get; private set; }
    private int currentPartsCount;
    private float lastPercentage;
    private Color lastColor;
    private Color[] currentTargetColors;
    private Cell[] currentSelection;
    private int selectionIndex = 0;

    public void ShowResultColor(Color clr) => currentCircle.SetResultColor(clr);

    public void SetCircle(Circle c)
    {
        currentCircle = c;
        lineManager = c.GetLinesManager();
        GetRandomColors();
    }

    public void EndGame(bool win)
    {
        InGame = false;
        game_UI_Controller.ShowEndGameWindow(win, currentTargetColors);
    }

    public void ChooseColor(Cell cell)
    {
        if (selectionIndex != currentPartsCount)
        {
            currentSelection[selectionIndex] = cell;
            cell.HideColor();
            currentCircle.SetPartColor(selectionIndex, cell.CellColor);
            lineManager.CurrentLine.SetCellColor(selectionIndex, cell.CellColor);
            if (selectionIndex != currentPartsCount)
                selectionIndex++;
        }
    }

    public void EraseColor()
    {
        if (selectionIndex > 0)
            selectionIndex--;
        if (currentSelection[selectionIndex] != null)
        {
            currentSelection[selectionIndex].ShowColor();
            lineManager.CurrentLine.EraseCellColor(selectionIndex);
            currentCircle.SetPartDefaultColor(selectionIndex);
        }
        currentSelection[selectionIndex] = null;
    }

    public void CheckColor()
    {
        if (lineManager.LineFull())
        {
            Color[] clr = new Color[currentPartsCount];

            for (int i = 0; i < currentPartsCount; i++)
            {
                clr[i] = currentSelection[i].CellColor;
            }

            lastColor = currentCircle.GetColor(clr);
            lastPercentage = CalculateColorMatch(currentCircle.TargetColor, lastColor);
            SetLineResult();
        }
    }

    private void SetLineResult()
    {
        lineManager.CurrentLine.SetIndicator(lastPercentage, lastColor);
        ShowResultColor(lastColor);
        CompareColors();
        lineManager.NextLine();
        ClearSelection();
    }

    private void CompareColors()
    {
        bool win = true;

        for (int i = 0; i < currentPartsCount; i++)
        {

            if (currentTargetColors[i] == currentSelection[i].CellColor)
            {
                lineManager.CurrentLine.SetCellSprite(i, true);
                currentSelection[i].ShowColor();
            }

            else if (currentTargetColors.Contains(currentSelection[i].CellColor))
            {
                lineManager.CurrentLine.SetCellSprite(i, false);
                currentSelection[i].ShowColor();
                win = false;
            }

            else
            {
                currentSelection[i].HideColor();
                win = false;
            }
        }

        if (win || lastPercentage >= 99.89f)
            EndGame(win);
    }

    private void ClearSelection()
    {
        selectionIndex = 0;
        for (int i = 0; i < currentPartsCount; i++)
        {
            currentSelection[i] = null;
        }

        currentCircle.ResetPartColor();
    }

    private void Awake()
    {
        instance = this;
        selectionIndex = 0;
        InGame = true;
        //GetRandomColors();
    }

    private void GetRandomColors()
    {
        List<Color> availableColors = new List<Color>(colors);
        currentPartsCount = currentCircle.GetPartsCount();
        currentTargetColors = new Color[currentPartsCount]; 
        currentSelection = new Cell[currentPartsCount];

        for (int i = 0; i < currentPartsCount; i++)
        {
            Color clr = availableColors[Random.Range(0, availableColors.Count)];
            currentTargetColors[i] = clr;
            availableColors.Remove(clr);
        }

        currentCircle.CreateColor(currentTargetColors);
    }

    private float CalculateColorMatch(Color color1, Color color2)
    {
        float maxDistance = 0.5f;
        Vector3 firstColor = new Vector3(color1.r, color1.g, color1.b);
        Vector3 secondColor = new Vector3(color2.r, color2.g, color2.b);
        float value = Vector3.Distance(firstColor, secondColor);
        float percentage = value > maxDistance ? 0 : 100 * (1 - (value / maxDistance)); ;
        return percentage;
    }
}
