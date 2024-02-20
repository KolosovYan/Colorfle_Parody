using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private Image cashedImage;
    [SerializeField] private TextMeshProUGUI percentageText;
    private Color currentColor;

    public void SetColor(Color clr)
    {
        cashedImage.color = clr;
        currentColor = clr;
    }

    public void SetText(float percentage) => percentageText.text = percentage.ToString("0.0") + "%";

    public void ClearIndicator()
    {
        percentageText.text = string.Empty;
        cashedImage.color = Color.white;
    }

    private void OnMouseDown()
    {
        Game_Controller.instance.ShowResultColor(currentColor);
    }
}
