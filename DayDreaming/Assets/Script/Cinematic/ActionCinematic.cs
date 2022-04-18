using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using UnityEngine.AI;

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
                        // Update Player NavAgent
                        if (movement.Agent.Equals(GameManager.instance.player.Agent))
                        {
                            movement.Agent.transform.position = GameManager.instance.player.transform.position;
                            GameManager.instance.player.transform.localPosition = Vector3.zero;
                        }
                        movement.Agent.SetDestination(movement.GoTo.position);
                        while(Vector3.SqrMagnitude(movement.Agent.nextPosition - movement.GoTo.position) > 0.03)
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
    public NavMeshAgent Agent;
    public Transform GoTo;
}

