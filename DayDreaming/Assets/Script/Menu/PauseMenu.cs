using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    // Permet d'accéder au panneau d'option
    public GameObject settingsWindow;
    public GameObject MenuPause;

    public void resume()
    {
        if(MenuPause.activeSelf){
            MenuPause.SetActive(false);
            Time.timeScale = 1;
        }
        else{
            if(!settingsWindow.activeSelf)
                OpenMenuPause();
            else{
                settingsWindow.SetActive(false);
                MenuPause.SetActive(true);
            }
        }
        
    }

    public void openSettings()
    {
        MenuPause.SetActive(false);
        settingsWindow.SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    // lance le menu pause
    public void OpenMenuPause()
    {
        Time.timeScale = 0;
        MenuPause.SetActive(true);
    }
}
