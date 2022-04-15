using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // UI
    public GameObject gameOverMenu;
    [SerializeField] private CardsFight CardsFight;

    // TODO : Animation
    public void OpenGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    // TODO Menu Pause
    // TODO Menu combat
    public void OpenFightMenu()
    {
        GameManager.instance.soundManager.music.Stop(false);
        CardsFight.Open();
    }
    // TODO HUD
}
