using UnityEngine.SceneManagement;
public class Scene_Loader 
{
    public static void LoadSceneByName(string sceneName) => SceneManager.LoadScene(sceneName);
}
