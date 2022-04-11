using UnityEngine;
using UnityEngine.InputSystem;


// Script pour le menu pause
// @author Maxime Ginda
public class PauseMenu : MonoBehaviour
{
    // Permet d'accéder au panneau d'option
    public GameObject settingsWindow;
    public GameObject pauseMenu;

    public void Resume()
    {
        if(pauseMenu.activeSelf){
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else{
            if(!settingsWindow.activeSelf)
                OpenMenuPause();
            else{
                settingsWindow.SetActive(false);
                pauseMenu.SetActive(true);
            }
        }
        
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(false);
        settingsWindow.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // lance le menu pause
    public void OpenMenuPause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
}
