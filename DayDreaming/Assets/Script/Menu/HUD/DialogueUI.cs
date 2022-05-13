using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public Image SpriteLeft;
    public Image SpriteRight;
    public TextMeshProUGUI TextMesh;

    private bool Next = false;
    private string TextToDisplay = "";
    [SerializeField] private float Speed = 0.05f;

    private void Awake()
    {
        Hide();
    }

    public void Display(string Text, Dialogue.Talk.SpriteDisplay SpriteDisplay, Sprite Sprite, Sprite Right)
    {
        TextToDisplay = Text;
        Next = false;
        StartCoroutine(DisplayCoroutine(SpriteDisplay, Sprite, Right));
    }

    private IEnumerator DisplayCoroutine(Dialogue.Talk.SpriteDisplay SpriteDisplay, Sprite Sprite, Sprite Right)
    {
        switch(SpriteDisplay)
        {
            case Dialogue.Talk.SpriteDisplay.None:
                SpriteRight.gameObject.SetActive(false);
                SpriteLeft.gameObject.SetActive(false);
                break;
            case Dialogue.Talk.SpriteDisplay.Left:
                SpriteRight.gameObject.SetActive(false);
                SpriteLeft.gameObject.SetActive(true);
                SpriteLeft.sprite = Sprite;
                break;
            case Dialogue.Talk.SpriteDisplay.Right:
                SpriteLeft.gameObject.SetActive(false);
                SpriteRight.gameObject.SetActive(true);
                SpriteRight.sprite = Sprite;
                break;
            case Dialogue.Talk.SpriteDisplay.Both:
                SpriteLeft.gameObject.SetActive(true);
                SpriteRight.gameObject.SetActive(true);
                SpriteLeft.sprite = Sprite;
                SpriteRight.sprite = Right;
                break;
        }

        yield return StartCoroutine(TextCoroutine());
    }

    public void Show()
    {
        transform.gameObject.SetActive(true);
    }
    public void Hide()
    {
        transform.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        if(TextMesh.text == TextToDisplay)
            Next = true;
        else
            TextMesh.text = TextToDisplay;
    }

    private IEnumerator TextCoroutine()
    {
        TextMesh.text = "";
        foreach (var c in TextToDisplay)
        {
            if(TextMesh.text != TextToDisplay)
            {
                TextMesh.text += c;
                yield return new WaitForSeconds(Speed);
            }
        }
    }

    public IEnumerator WaitForDialogueEnd()
    {
        while (!Next)
            yield return new WaitForSeconds(Time.deltaTime);
    }
}
