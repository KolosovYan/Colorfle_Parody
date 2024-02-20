using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_UI_Controller : MonoBehaviour
{
    public void OnPlayPressed() => Scene_Loader.LoadSceneByName("Game");
}
