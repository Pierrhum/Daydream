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
                    List<NavMeshAgent> MovingAgents = new List<NavMeshAgent>();
                    List<Vector3> ToGoPositions = new List<Vector3>();

                    foreach (Movement movement in Movements.Value)
                    {
                        // Update Player NavAgent
                        if (movement.Agent.Equals(GameManager.instance.player.Agent))
                        {
                            movement.Agent.transform.position = GameManager.instance.player.transform.position;
                            GameManager.instance.player.transform.localPosition = Vector3.zero;
                        }
                        if(movement.ShouldTeleport)
                        {
                            movement.Agent.Warp(movement.GoTo.position);
                            yield return new WaitForSeconds(1f);
                        } 
                        else
                        {
                            movement.Agent.SetDestination(movement.GoTo.position);
                            MovingAgents.Add(movement.Agent);
                            ToGoPositions.Add(movement.GoTo.position);
                        }
                    }

                    // Check si tous les agents sont bien arriv�s, � refaire c'est un peu d�gueu
                    for (int i = 0; i < MovingAgents.Count; i++)
                    {
                        while (Vector3.SqrMagnitude(MovingAgents[i].nextPosition - ToGoPositions[i]) > 0.03)
                            yield return new WaitForSeconds(Time.deltaTime);
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
    public bool ShouldTeleport = false;
    public NavMeshAgent Agent;
    public Transform GoTo;
}

