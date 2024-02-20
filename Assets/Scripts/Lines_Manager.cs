using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines_Manager : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private Line[] lines;

    public Line CurrentLine { get; private set; }
    private int lineIndex;

    private void Start()
    {
        lineIndex = 0;
        CurrentLine = lines[lineIndex];
    }
    
    public void NextLine()
    {
        lineIndex++;

        if (lineIndex < lines.Length)
            CurrentLine = lines[lineIndex];

        else if (Game_Controller.instance.InGame)
            Game_Controller.instance.EndGame(false);
    }

    public bool LineFull()
    {
        if (!CurrentLine.HasEmptyCells())
            return true;
        else
            return false;
    }
}
