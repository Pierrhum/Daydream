using UnityEngine;
using UnityEngine.SceneManagement;

// Script pour le menu de game over
// @author Maxime Ginda
public class GameOverMenu : MonoBehaviour
{

    // GameObject à détruire
    public GameObject settingsWindow;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        Destroy(settingsWindow);
        Destroy(pauseMenu);
    }

    // Relance la scène courante
    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Retourne au menu principale
    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    // Permet de quitter le jeu
    public void QuitGame()
    {
        Application.Quit();
    }

}
