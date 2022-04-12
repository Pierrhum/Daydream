using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsFight : MonoBehaviour
{
    public Image Overlay;
    public GameObject PlayerHand;

    public void Start()
    {
        SetOpacity(0.0f);
        Utils.UI.SetImageOpacity(Overlay, 0.0f);
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowCoroutine());
    }

    private IEnumerator ShowCoroutine()
    {
        yield return StartCoroutine(Utils.UI.Fade(Overlay, 0.0f, 1.0f, 0.5f));
        SetOpacity(1.0f);
    }

    private void SetOpacity(float a)
    {
        foreach (Transform child in PlayerHand.transform)
        {
            Utils.UI.SetImageOpacity(child.GetComponent<Image>(), a);
        }
    }
}
