using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Coffee.UIEffects;
using TMPro;

namespace Utils {
    public static class UI
    {
        #region Basic Unity Effects
        public class CoroutineWrapper : MonoBehaviour { }

        public static void SetImageOpacity(Image image, float a)
        {
            Color c = image.color;
            image.color = new Color(c.r, c.g, c.b, a);
        }
        public static IEnumerator Fade(List<Image> images, float start, float end, float duration)
        {
            float timer = 0f;
            AnimationCurve smoothCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 1f) });

            while (timer <= duration)
            {
                timer += Time.deltaTime;
                foreach(Image img in images)
                {
                    Color c = img.color;
                    img.color = new Color(c.r, c.g, c.b, Mathf.Lerp(start, end, smoothCurve.Evaluate(timer / duration)));
                }
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        public static IEnumerator Fade(List<Image> images, List<TextMeshProUGUI> texts, float start, float end, float duration)
        {
            float timer = 0f;
            AnimationCurve smoothCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 1f) });

            while (timer <= duration)
            {
                timer += Time.deltaTime;
                foreach (Image img in images)
                {
                    Color c = img.color;
                    img.color = new Color(c.r, c.g, c.b, Mathf.Lerp(start, end, smoothCurve.Evaluate(timer / duration)));
                }
                foreach(TextMeshProUGUI txt in texts)
                {
                    Color c = txt.color;
                    txt.color = new Color(c.r, c.g, c.b, Mathf.Lerp(start, end, smoothCurve.Evaluate(timer / duration)));
                }
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        #endregion

        #region UiEffect Tool

        public static IEnumerator Dissolve(UIDissolve dissolve, float start, float end, float duration)
        {
            float timer = 0f;
            AnimationCurve smoothCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 1f) });

            while (timer <= duration)
            {
                timer += Time.deltaTime;
                dissolve.effectFactor = Mathf.Lerp(start, end, smoothCurve.Evaluate(timer / duration));
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }


        public static IEnumerator BrightCoroutine(UICard uICard, float duration)
        {
            float timer = 0f;
            AnimationCurve smoothCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 1f) });
            while(true)
            {
                while (timer <= duration)
                {
                    if(uICard.isSelected)
                        yield return new WaitForSeconds(Time.deltaTime);
                    else
                    {
                        timer += Time.deltaTime;
                        uICard.uIShiny.effectFactor = Mathf.Lerp(0.0f, 1.0f, smoothCurve.Evaluate(timer / duration));
                        yield return new WaitForSeconds(Time.deltaTime);
                    }
                }
                timer = 0f;
                yield return new WaitForSeconds(3f);
            }
        }

        public static IEnumerator GameOverAnim(UIHsvModifier hsv, UIDissolve playerSprite, float duration)
        {
            float timer = 0f;
            AnimationCurve smoothCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 1f) });
            while (timer <= duration)
            {
                timer += Time.deltaTime;
                float HsvLerp = Mathf.Lerp(0.0f, -0.5f, smoothCurve.Evaluate(timer / duration));
                float DissolveLerp = Mathf.Lerp(0.0f, 1.0f, smoothCurve.Evaluate(timer / duration));

                hsv.saturation = HsvLerp;
                hsv.value = HsvLerp;
                playerSprite.effectFactor = DissolveLerp;

                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

        #endregion
    }
};

