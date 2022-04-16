using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image Fill;

    public void SetOpacity(float a)
    {
        foreach (Image img in transform.GetComponentsInChildren<Image>())
            img.color = new Color(img.color.r, img.color.g, img.color.b, a);
    }

    public void SetFill(float fillAmount)
    {
        StartCoroutine(FillCoroutine(Fill.fillAmount, fillAmount, 0.2f));
    }

    private IEnumerator FillCoroutine(float start, float end, float duration)
    {
        float timer = 0f;
        AnimationCurve smoothCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 1f) });

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            Fill.fillAmount = Mathf.Lerp(start, end, smoothCurve.Evaluate(timer / duration));
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
