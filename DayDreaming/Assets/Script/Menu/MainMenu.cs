using UnityEngine;
using UnityEngine.SceneManagement;

// Script pour le menu principal
// @author Maxime Ginda
public class MainMenu : MonoBehaviour
{
    // Premier niveau lorsqu'on lance le jeu
    public string firstLevel;

    // Permet d'accéder au panneau d'option
    public GameObject settingsWindow;

    // Permet de lancer le jeu
    public void startGame()
    {
        SceneManager.LoadScene(this.firstLevel);
    }

    // Permet d'ouvrir les settings du jeu 
    public void openSettings()
    {
        settingsWindow.SetActive(true);
    }

    // Permet de quitter le jeu
    public void quitGame()
    {
        Application.Quit();
    }
}
