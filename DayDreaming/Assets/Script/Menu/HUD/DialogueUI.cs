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

    private void Awake()
    {
        Hide();
    }

    public IEnumerator Display(string Text, Dialogue.Talk.SpriteDisplay SpriteDisplay, Sprite Sprite, Sprite Right)
    {
        yield return StartCoroutine(DisplayCoroutine(Text, SpriteDisplay, Sprite, Right));
    }

    private IEnumerator DisplayCoroutine(string Text, Dialogue.Talk.SpriteDisplay SpriteDisplay, Sprite Sprite, Sprite Right)
    {
        switch(SpriteDisplay)
        {
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

        yield return StartCoroutine(TextCoroutine(Text));
    }

    public void Show()
    {
        transform.gameObject.SetActive(true);
    }
    public void Hide()
    {
        transform.gameObject.SetActive(false);
    }

    private IEnumerator TextCoroutine(string Text)
    {
        TextMesh.text = "";
        foreach (var c in Text)
        {
            TextMesh.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
