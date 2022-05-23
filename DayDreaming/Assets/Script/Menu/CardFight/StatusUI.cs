using Coffee.UIEffects;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    public Image StatusImage;
    public TextMeshProUGUI TurnRemaining;

    // Status Info
    public GameObject StatusInfo;
    public TextMeshProUGUI Description;
    public Image Card;
    public Image StatusInfoImage;
    public TextMeshProUGUI TurnRemainingInfo;
    public float Transition = 2f;

    private string statusEffectDescription;
    private Coroutine lastC;
    private UIGradient gradient;

    public void UpdateStatusUI(Status status, int Turn)
    {
        StatusImage.sprite = status.GetStatusSprite();
        TurnRemaining.SetText("" + Turn);

        statusEffectDescription = status.GetDescription();
        Card.sprite = status.GetCardSprite();
        StatusInfoImage.sprite = StatusImage.sprite;
        gradient = StatusInfo.GetComponent<UIGradient>();

    }

    public void HideInfo()
    {
        if(lastC != null)
            StopCoroutine(lastC);
        StatusInfo.gameObject.SetActive(false);
    }
    public void ShowInfo()
    {
        StatusInfo.gameObject.SetActive(true);

        Description.SetText(statusEffectDescription);
        TurnRemainingInfo.SetText(TurnRemaining.text.ToString());

        lastC = StartCoroutine(InfoCoroutine());
    }

    private IEnumerator InfoCoroutine()
    {
        AnimationCurve smoothCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 1f) });
        float timer = 0f;
        var r = UnityEngine.Random.Range(0, 2);
        Color targetColor = new Color(r == 0 ? 255 : 0, r == 1 ? 255 : 0, r == 2 ? 255 : 0);
        while (true)
        {
            timer += Time.deltaTime;

            var R = Mathf.Lerp(gradient.color1.r, targetColor.r, smoothCurve.Evaluate(timer / Transition));
            var G = Mathf.Lerp(gradient.color1.g, targetColor.g, smoothCurve.Evaluate(timer / Transition));
            var B = Mathf.Lerp(gradient.color1.b, targetColor.b, smoothCurve.Evaluate(timer / Transition));
            gradient.color1 = new Color(R, G, B);

            if (timer >= Transition)
            {
                r = UnityEngine.Random.Range(0, 2);
                targetColor = new Color(r==0 ? 255 : 0, r == 1 ? 255 : 0, r == 2 ? 255 : 0);
                timer = 0f;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
