using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string sceneToLoad;

    /// <summary>
    /// On Button press, go to Level 01 (Function added to Button)
    /// </summary>
    public void GoToLevelOne()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
