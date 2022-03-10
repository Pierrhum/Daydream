using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Permet d'accéder au panneau d'option
    public GameObject settingsWindow;
    public GameObject MenuPause;

    public void resume()
    {
        if(MenuPause.activeSelf){
            MenuPause.SetActive(false);
        }
        else OpenMenuPause();
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
        MenuPause.SetActive(true);
    }
}
