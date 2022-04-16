using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : Fighter
{
    public float speed = 5f;
    private Rigidbody2D rbody;
    private IsometricCharacterRenderer isoRenderer;

    public Quest Quest;

    public bool isFighting = false;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }

    /************ COMBAT ***********/

    public override void CanPlay(bool canPlay)
    {
        base.CanPlay(canPlay);
        foreach (UICard uiCard in GameManager.instance.uiManager.CardsFight.PlayerHand.UICards)
            uiCard.GetComponent<Button>().interactable = canPlay;
    }

    public void NextQuest()
    {
        if (Quest.Next == null)
            Debug.Log("jeu terminé");
        else
        {
            Quest = Quest.Next;
            Debug.Log("New quest : " + Quest.Name);
        }
    }

    /************ INPUTS ***********/

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 currentPos = rbody.position;
        Vector2 inputVector = context.ReadValue<Vector2>();
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * speed;

        isoRenderer.SetDirection(movement);
        rbody.velocity = movement;
    }

    public void OpenMenu(InputAction.CallbackContext context)
    {
        Debug.Log("yo");   
    }
}
