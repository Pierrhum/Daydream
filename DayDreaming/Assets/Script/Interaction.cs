using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [SerializeField] private List<InteractionEffect> effects;

    private Canvas canvas;
    private Collider2D playerCollider;
    private bool isColliding;

    private void Start()
    {
        canvas = transform.parent.GetComponentInChildren<Canvas>();
        playerCollider = GameManager.instance.player.GetComponentInChildren<Collider2D>();
        isColliding = false;
    }

    private void Update()
    {
        if (isColliding && Keyboard.current[Key.Space].isPressed)
        {
            GameManager.instance.player.StopMoving();
            canvas.enabled = false;
            StartCoroutine(Interact());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.Equals(playerCollider))
        {
            canvas.enabled = true;
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.Equals(playerCollider))
        {
            canvas.enabled = false;
            isColliding = false;
        }
    }

    private IEnumerator Interact()
    {
        foreach(InteractionEffect effect in effects)
        {
            yield return StartCoroutine(effect.Activate());
        }

        GameManager.instance.player.CanMove = true;
    }

    [System.Serializable] private class InteractionEffect
    {
        private enum Type { None, Dialogue, Teleport, Item }

        [SerializeField] private Type effectType = Type.None;
        [ConditionalField(nameof(effectType),false,Type.Dialogue)] public Dialogue dialogue;
        [ConditionalField(nameof(effectType),false,Type.Teleport)] public Teleport teleport;
        [ConditionalField(nameof(effectType),false,Type.Item)] public Item item;

        public IEnumerator Activate()
        {
            switch(effectType)
            {
                case Type.Dialogue:

                    UIManager UIManager = GameManager.instance.uiManager;

                    UIManager.OpenDialogueUI(false);

                    foreach (Dialogue.Talk talk in dialogue.talks)
                    {
                        UIManager.HUD.DialogueUI.Display(talk.Text, talk.Display, talk.sprite, talk.Right);

                        yield return UIManager.HUD.DialogueUI.WaitForDialogueEnd();
                    }

                    UIManager.CloseDialogueUI(false);

                    break;

                case Type.Teleport:

                    GameManager.instance.ChangeScene(teleport.from,teleport.to);

                    GameManager.instance.player.GetComponentInParent<NavMeshAgent>().enabled = false;
                    GameManager.instance.player.transform.parent.transform.position = teleport.location;
                    GameManager.instance.player.GetComponentInParent<NavMeshAgent>().enabled = true;

                    break;
                    
                case Type.Item:

                    break;
            }
        }
    }

    [System.Serializable] private class Teleport
    {
        [SerializeField] public GameManager.Scene from;
        [SerializeField] public GameManager.Scene to;
        [SerializeField] public Vector3 location;
    }

    [System.Serializable] private class Item { }
}