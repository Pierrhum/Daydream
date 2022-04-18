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
            GameManager.instance.soundManager.StopMusic(false);
        HUD.DialogueUI.Show();
    }

    public void CloseDialogueUI(bool playMusic)
    {
        if (playMusic)
            GameManager.instance.soundManager.PlayMusic(SoundManager.MusicType.Main);
        HUD.DialogueUI.Hide();
    }
}
