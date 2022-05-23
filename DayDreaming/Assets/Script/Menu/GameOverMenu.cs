using Coffee.UIEffects;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{

    // GameObject à détruire
    public GameObject settingsWindow;
    public GameObject pauseMenu;

    public List<Image> FadeImages;
    public List<TextMeshProUGUI> FadeTexts;

    // Start is called before the first frame update
    private void Start()
    {
        foreach (Image img in FadeImages)
        {
            Color c = img.color;
            img.color = new Color(c.r, c.g, c.b, 0);
        }
        foreach (TextMeshProUGUI txt in FadeTexts)
        {
            Color c = txt.color;
            txt.color = new Color(c.r, c.g, c.b, 0);
        }
        gameObject.SetActive(false);
    }

    public IEnumerator Open(UIHsvModifier hsv, UIDissolve dissolve)
    {
        gameObject.SetActive(true);
        yield return StartCoroutine(Utils.UI.GameOverAnim(hsv, dissolve, 2f));
        Destroy(settingsWindow);
        Destroy(pauseMenu);
        yield return StartCoroutine(Utils.UI.Fade(FadeImages, FadeTexts, 0.0f, 1.0f, 2.0f));
    }

    // Relance la scène courante
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Retourne au menu principale
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Permet de quitter le jeu
    public void QuitGame()
    {
        Application.Quit();
    }

}
