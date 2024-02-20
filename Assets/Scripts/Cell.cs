using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private Image cashedImage;
    [SerializeField] private Image frameImage;
    [SerializeField] private Sprite frameDefault;
    [SerializeField] private Sprite frameDiagonal;

    public Color CellColor { get; private set; }
    [SerializeField] private bool isInteractive;
    private bool canTouch = true;
    private Game_Controller controller;

    public void SetInteractiveState(bool state) => isInteractive = state;

    public void SetColor(Color color) => cashedImage.color = color;

    public void SetFrameSprite(Sprite spr) => frameImage.sprite = spr;

    public void SetCanTouch(bool state) => canTouch = state;

    public void HideColor()
    {
        SetFrameSprite(frameDiagonal);
        cashedImage.color = new Color(cashedImage.color.r, cashedImage.color.g, cashedImage.color.b, 0.3f);
        SetInteractiveState(false);
    }

    public void ShowColor()
    {
        SetFrameSprite(frameDefault);
        cashedImage.color = new Color(cashedImage.color.r, cashedImage.color.g, cashedImage.color.b, 1.0f);
        SetInteractiveState(true);
    }

    private void Start()
    {
        CellColor = cashedImage.color;
        controller = Game_Controller.instance;
        ShowColor();
    }

    private void OnMouseDown()
    {
        if (canTouch && isInteractive && Game_Controller.instance.InGame)
        {
            controller.ChooseColor(this as Cell);
        }
    }
}
