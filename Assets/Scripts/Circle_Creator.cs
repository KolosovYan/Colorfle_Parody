using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle_Creator : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private Game_Controller game_Controller;
    [SerializeField] private RectTransform canvas;

    [Header("Prefabs")]

    [SerializeField] private List<Circle> circlePrefabs;

    private void Start()
    {
        CreateCircle();
    }

    private void CreateCircle()
    {
        Circle c = Instantiate(GetRandomCircle(), canvas);
        c.transform.SetAsFirstSibling();

        game_Controller.SetCircle(c);
    }

    private Circle GetRandomCircle()
    {
        return circlePrefabs[Random.Range(0, circlePrefabs.Count)];
    }
}
