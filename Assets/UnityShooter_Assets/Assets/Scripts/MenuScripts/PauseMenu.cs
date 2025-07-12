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

    
    private void Update()
    {
        PauseMenuButton();
    }

    /// <summary>
    /// When player presses the Escape key (Esc) the game pauses
    /// </summary>
    private void PauseMenuButton()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused == false)
            {
                Pause();
            }
        }
    }

    /// <summary>
    /// Function to Pause the Menu
    /// </summary>
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        soundVolume.SetVolume();
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    /// <summary>
    /// Function to Resume the game (Unpause game)
    /// </summary>
    private void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    /// <summary>
    /// Clicking on Resume button to unpause game (Function added to Button)
    /// </summary>
    public void PressedResume()
    {
        Resume();
    }

    /// <summary>
    /// Clicked on Options button (Function added to Button)
    /// </summary>
    public void PressedOptions()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    /// <summary>
    /// Clicked on Back to Pause Menu button in Options (Function added to Button)
    /// </summary>
    public void PressedBackToPauseMenu()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    /// <summary>
    /// Clicked on Return to Main Menu button. A Comfirm UI will appear first (Function added to Button)
    /// </summary>
    public void PressedReturnToMenu()
    {
        pauseMenuUI.SetActive(false);
        comfirmMenuUI.SetActive(true);
    }

    /// <summary>
    /// Player pressed Yes button in ComfirmMenu (Function added to Button)
    /// </summary>
    public void PressedYes()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(sceneToLoad);
    }

    /// <summary>
    /// Player pressed No button in ComfirmMenu (Function added to Button)
    /// </summary>
    public void PressedNo()
    {
        comfirmMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}
