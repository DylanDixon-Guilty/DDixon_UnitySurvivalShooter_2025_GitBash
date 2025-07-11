using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string sceneToLoad;

    //On Button press, go to Level 01
    public void GoToLevelOne()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
