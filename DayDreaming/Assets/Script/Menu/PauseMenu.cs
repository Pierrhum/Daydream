using UnityEngine;
using UnityEngine.InputSystem;


// Script pour le menu pause
// @author Maxime Ginda
public class PauseMenu : MonoBehaviour
{
    // Permet d'accéder au panneau d'option
    public GameObject settingsWindow;
    public GameObject pauseMenu;
    public GameObject HUD;
    public GameObject inventory;

    public void Resume()
    {
        if(pauseMenu.activeSelf){
            pauseMenu.SetActive(false);
            HUD.SetActive(true);
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
        HUD.SetActive(false);
        if(inventory.activeSelf){
            inventory.SetActive(false);
        }
        pauseMenu.SetActive(true);
    }
}
