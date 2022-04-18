using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using UnityEngine.AI;

[System.Serializable]
public class ActionCinematic
{
    public enum Type { None, Dialogue, Movement, PlayMusic, StopMusic, SFX, Wait, Reward, QuestItem }
    public Type ActionType = Type.None;

    [ConditionalField(nameof(ActionType), false, Type.Dialogue)]
    public Dialogue Dialogue;

    [ConditionalField(nameof(ActionType), false, Type.Movement)]
    public CollectionWrapper<Movement> Movements;

    [ConditionalField(nameof(ActionType), false, Type.PlayMusic)]
    public Music Music;
    private static Music CurrentMusic;
    [ConditionalField(nameof(ActionType), false, Type.StopMusic)]
    public bool FadeOut = false;

    [ConditionalField(nameof(ActionType), false, Type.SFX)]
    public AudioClip SFX;
    [ConditionalField(nameof(ActionType), false, Type.SFX)]
    public bool WaitEnd;

    [ConditionalField(nameof(ActionType), false, Type.Reward)]
    public Reward Reward;

    [ConditionalField(nameof(ActionType), false, Type.QuestItem)]
    public QuestItem QuestItem;


    [ConditionalField(nameof(ActionType), false, Type.Wait)]
    public float SecondsToWait = 1.0f;

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
                            GameManager.instance.player.transform.localPosition = new Vector3(0.01f,0f,0.07f);
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

                    // Check si tous les agents sont bien arrivés, à refaire c'est un peu dégueu
                    for (int i = 0; i < MovingAgents.Count; i++)
                    {
                        while (Vector3.SqrMagnitude(MovingAgents[i].nextPosition - ToGoPositions[i]) > 0.03)
                            yield return new WaitForSeconds(Time.deltaTime);
                    }
                }
                break;

            case Type.PlayMusic:
                CurrentMusic = Music;
                GameManager.instance.soundManager.StopMusic(false);
                GameManager.instance.soundManager.PlayMusic(CurrentMusic);
                break;

            case Type.StopMusic:
                CurrentMusic.Stop(FadeOut);
                yield return CurrentMusic.WaitForMusicEnd();
                GameManager.instance.soundManager.PlayMusic(SoundManager.MusicType.Main);
                break;

            case Type.SFX:
                yield return GameManager.instance.soundManager.Play2DSFX(SFX, WaitEnd);
                break;

            case Type.Reward:
                Reward.GiveRewardToPlayer();
                break;

            case Type.QuestItem:
                yield return QuestItem.Show();
                while (!QuestItem.isHidden())
                    yield return new WaitForSeconds(Time.deltaTime);
                break;

            case Type.Wait:
                yield return new WaitForSeconds(SecondsToWait);
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

[System.Serializable]
public class Reward
{
    public List<CardAsset> Cards;

    public void GiveRewardToPlayer()
    {
        Player player = GameManager.instance.player;
        foreach (CardAsset card in Cards)
            player.InventoryCards.Add(card);

        /// A SUPPRIMER PLUS TARD, UNE FOIS QU'ON POURRA SELECTIONNER NOS CARTES DANS L'INVENTAIRE
        foreach (CardAsset card in Cards)
            player.FightCards.Add(card);
        ///

        // Donner XP
    }
}

