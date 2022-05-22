using Coffee.UIEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightFeedback : MonoBehaviour
{
    private Image sprite;
    private UIEffect uIEffect;
    private UIGradient uIGradient;

    public bool isAnimating = false;

    private void Awake()
    {
        sprite = GetComponent<Image>();
        uIEffect = GetComponent<UIEffect>();
        uIGradient = GetComponent<UIGradient>();
    }

    public IEnumerator Hurt(float duration)
    {
        isAnimating = true;
        float timer = 0f;
        sprite.color = Color.red;

        while (timer <= duration)
        {
            timer += 0.1f;
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        sprite.color = Color.white;
        sprite.enabled = true;
        isAnimating = false;
    }

    public IEnumerator Heal(float duration)
    {
        isAnimating = true;
        float timer = 0f;
        AnimationCurve smoothCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(duration/2, 1f), new Keyframe(duration, 0f) });

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            uIGradient.offset = Mathf.Lerp(1.0f, -1.0f, smoothCurve.Evaluate(timer / duration));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        isAnimating = false;
    }
}
