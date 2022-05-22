using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.AI;
using Coffee.UIEffects;

public class Player : Fighter
{
    public NavMeshAgent Agent;
    public float speed = 5f;
    private Rigidbody2D rbody;
    private IsometricCharacterRenderer isoRenderer;
    public List<CardAsset> InventoryCards;

    public Quest Quest;
    public int MaxFightCards = 5;

    public bool CanMove = true;
    public bool isFighting = false;

    [System.NonSerialized]
    public CardAsset SelectedCard;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();

        CardsFightUI = GameManager.instance.uiManager.CardsFight;
    }

    public void StopMoving()
    {
        CanMove = false;
        rbody.velocity = Vector2.zero;
        isoRenderer.SetDirection(Vector2.zero);
    }

    /************ COMBAT ***********/

    public override void CanPlay(bool canPlay)
    {
        base.CanPlay(canPlay);
        foreach (UICard uiCard in CardsFightUI.PlayerHand.UICards)
            uiCard.GetComponent<Button>().interactable = canPlay;
    }

    public void NextQuest()
    {
        if (Quest.Next == null)
            Debug.Log("jeu terminé");
        else
        {

            Quest = Quest.Next;
            GameObject.Find("QuestToDo").GetComponent<TMPro.TextMeshProUGUI>().text = Quest.Name;
            Debug.Log("New quest : " + Quest.Name);
        }
    }

    /************ INPUTS ***********/

    public void Move(InputAction.CallbackContext context)
    {
        if(CanMove)
        {
            Vector2 currentPos = rbody.position;
            Vector2 inputVector = context.ReadValue<Vector2>();
            inputVector = Vector2.ClampMagnitude(inputVector, 1);
            Vector2 movement = inputVector * speed;

            isoRenderer.SetDirection(movement);
            rbody.velocity = movement;
        }
    }

    public void OpenMenu(InputAction.CallbackContext context)
    {
        Debug.Log("yo");   
    }

    public override IEnumerator Attack(Fighter other)
    {
        // Apply card effect
        if (SelectedCard.rarity == CardAsset.Rarity.UNIQUE)
        {
            if (SelectedCard.AnimationID != -1)
            {
                var template = CardsFightUI.ImagesManifold[SelectedCard.AnimationID];
                var AnimationImage = Instantiate<Image>(template, template.transform.position, template.transform.rotation, CardsFightUI.transform);
                yield return StartCoroutine(CardsFightUI.CurvesManifold[SelectedCard.AnimationID].FollowCurve(AnimationImage, false));
            }
        }
        yield return StartCoroutine(Attack(SelectedCard, other));
    }

    public override void Die()
    {
        // Replace UIEffects for Overlay and PlayerSprite
        DestroyImmediate(CardsFightUI.Overlay.GetComponent<UIDissolve>());
        UIHsvModifier hsv = CardsFightUI.Overlay.gameObject.AddComponent<UIHsvModifier>();
        DestroyImmediate(Feedback.GetComponent<UIEffect>());
        UIDissolve dissolve = Feedback.gameObject.AddComponent<UIDissolve>();

        CardsFightUI.Enemy.canPlay = false;
        canPlay = false;
        foreach (UICard uiCard in CardsFightUI.PlayerHand.UICards)
            uiCard.GetComponent<Button>().interactable = canPlay;

        StartCoroutine(Utils.UI.GameOverAnim(hsv, dissolve, 2f));

    }
}
