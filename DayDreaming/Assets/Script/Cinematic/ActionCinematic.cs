using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

[System.Serializable]
public class ActionCinematic
{
    public enum Type { None, Dialogue, Movement }
    public Type ActionType = Type.None;

    [ConditionalField(nameof(ActionType), false, Type.Dialogue)]
    public Dialogue Dialogue;

    [ConditionalField(nameof(ActionType), false, Type.Movement)]
    public CollectionWrapper<Movement> Movements;

    public IEnumerator ProcessAction()
    {
        switch(ActionType)
        {
            case Type.Dialogue:
                {
                    GameManager.instance.uiManager.OpenDialogueUI(false);
                    foreach(Dialogue.Talk talk in Dialogue.talks)
                    {
                        DialogueUI DialogueUI = GameManager.instance.uiManager.HUD.DialogueUI;
                        DialogueUI.Display(talk.Text, talk.Display, talk.sprite, talk.Right);
                        yield return DialogueUI.WaitForDialogueEnd();
                    }
                    GameManager.instance.uiManager.CloseDialogueUI(false);
                }
                break;

            case Type.Movement:
                {
                    foreach (Movement movement in Movements.Value)
                    {
                        movement.Entity.transform.position = movement.GoTo.position;
                        yield return new WaitForSeconds(0.5f);
                    }
                }
                break;

            default:
                yield return null;
                break;
        }
    }
}

[System.Serializable]
public class Movement
{
    public GameObject Entity;
    public Transform GoTo;
}

