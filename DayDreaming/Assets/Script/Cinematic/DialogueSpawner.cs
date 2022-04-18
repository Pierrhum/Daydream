using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSpawner : MonoBehaviour
{
    public Dialogue Dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.Equals(GameManager.instance.player.GetComponentInChildren<Collider2D>()))
        {
            GameManager.instance.player.StopMoving();
            StartCoroutine(Play());
        }
    }

    public IEnumerator Play()
    {
        GameManager.instance.uiManager.OpenDialogueUI(false);
        foreach (Dialogue.Talk talk in Dialogue.talks)
        {
            DialogueUI DialogueUI = GameManager.instance.uiManager.HUD.DialogueUI;
            DialogueUI.Display(talk.Text, talk.Display, talk.sprite, talk.Right);
            yield return DialogueUI.WaitForDialogueEnd();
        }
        GameManager.instance.uiManager.CloseDialogueUI(false);
        GameManager.instance.player.CanMove = true;
    }
}
