using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // UI
    public GameObject gameOverMenu;
    public CardsFight CardsFight;
    public HUD HUD;

    // TODO : Animation
    public void OpenGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    // TODO Menu Pause
    // TODO Menu combat
    public void OpenFightMenu(Enemy Enemy)
    {
        GameManager.instance.soundManager.music.Stop(false);
        CardsFight.Open(Enemy);
    }

    public void CloseFightMenu()
    {
        CardsFight.Close();
    }
    // TODO HUD

    public void OpenDialogueUI(bool stopMusic)
    {
        if(stopMusic)
            GameManager.instance.soundManager.music.Stop(false);
        HUD.DialogueUI.Show();
    }

    public void CloseDialogueUI(bool playMusic)
    {
        if (playMusic)
            GameManager.instance.soundManager.music.Play();
        HUD.DialogueUI.Hide();
    }
}
