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
    public void StartGame()
    {
        SceneManager.LoadScene(this.firstLevel);
    }

    // Permet d'ouvrir les settings du jeu 
    public void OpenSettings()
    {
        settingsWindow.SetActive(true);
    }

    // Permet de quitter le jeu
    public void QuitGame()
    {
        Application.Quit();
    }
}
