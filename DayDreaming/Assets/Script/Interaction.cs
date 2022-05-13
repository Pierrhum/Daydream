using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Dialogue effect;

    private Canvas canvas;
    private Collider2D playerCollider;
    private bool isColliding;

    private void Awake()
    {
        canvas = transform.parent.GetComponentInChildren<Canvas>();
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Collider2D>();
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
        UIManager UIManager = GameManager.instance.uiManager;

        UIManager.OpenDialogueUI(false);

        foreach(Dialogue.Talk talk in effect.talks)
        {
            UIManager.HUD.DialogueUI.Display(talk.Text,talk.Display,talk.sprite,talk.Right);

            yield return UIManager.HUD.DialogueUI.WaitForDialogueEnd();
        }

        UIManager.CloseDialogueUI(false);

        GameManager.instance.player.CanMove = true;
    }
}