using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circle : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private Image targetColorBase;
    [SerializeField] private Image resultColorBase;
    [SerializeField] private List<Circle_Part> circleParts;
    [SerializeField] private Color defaultPartColor;
    [SerializeField] private Lines_Manager lines_Manager;

    public Color TargetColor { get; private set; }

    [System.Serializable]
    public class Circle_Part
    {
        [SerializeField] private Image partImage;
        [SerializeField] public int partPercentage;

        public void SetPartColor(Color color) => partImage.color = color;
    }

    public void SetPartColor(int partIndex, Color color) => circleParts[partIndex].SetPartColor(color);

    public void ResetPartColor()
    {
        foreach (Circle_Part part in circleParts)
            part.SetPartColor(defaultPartColor);
    }

    public void SetPartDefaultColor(int partIndex) => circleParts[partIndex].SetPartColor(defaultPartColor);

    public void SetResultColor(Color clr)
    {
        resultColorBase.enabled = false;
        resultColorBase.color = clr;
        StopAllCoroutines();
        StartCoroutine(HideOnClick());
    }

    public Lines_Manager GetLinesManager()
    {
        return lines_Manager;
    }

    public int GetPartsCount()
    {
        return circleParts.Count;
    }

    public void CreateColor(Color[] colors)
    {
        TargetColor = GetColor(colors);
        targetColorBase.color = TargetColor;
    }

    public Color GetColor(Color[] colors)
    {
        Color clr = new (0f, 0f, 0f, 1f);

        for (int i = 0; i < circleParts.Count; i++)
        {
            clr += ((colors[i] / 100) * circleParts[i].partPercentage);
        }

        clr.a = 1f;
        return clr;
    }

    private IEnumerator HideOnClick()
    {
        resultColorBase.enabled = true;

        yield return new WaitForSeconds(0.1f);

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        resultColorBase.enabled = false;
    }
}
