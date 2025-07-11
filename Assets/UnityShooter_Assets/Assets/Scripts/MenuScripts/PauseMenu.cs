using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject comfirmMenuUI;
    public GameObject optionsMenuUI;
    public SoundVolume soundVolume;

    //To change to main menu upon confirm
    public string sceneToLoad;

    // Update is called once per frame
    void Update()
    {
        PauseMenuButton();
    }

    //When player presses the Escape key (Esc) the game pauses
    void PauseMenuButton()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused == false)
            {
                Pause();
            }
        }
    }

    //Pause the Menu
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        soundVolume.SetVolume();
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    //Resume to game (Unpause game)
    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    //clicking on Resume button is way to unpause game
    public void PressedResume()
    {
        Resume();
    }

    //Clicked on Options button
    public void PressedOptions()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    //Clicked on Back to Pause Menu button in Options
    public void PressedBackToPauseMenu()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    //Clicked on Return to Main Menu button. A Comfirm UI will appear first
    public void PressedReturnToMenu()
    {
        pauseMenuUI.SetActive(false);
        comfirmMenuUI.SetActive(true);
    }

    //Player pressed Yes button in ComfirmMenu
    public void PressedYes()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(sceneToLoad);
    }

    //Player pressed No button in ComfirmMenu
    public void PressedNo()
    {
        comfirmMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}
