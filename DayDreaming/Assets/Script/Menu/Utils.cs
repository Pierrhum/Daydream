using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utils {
    public static class UI
    {
        public static void SetImageOpacity(Image image, float a)
        {
            Color c = image.color;
            image.color = new Color(c.r, c.g, c.b, a);
        }
        public static IEnumerator Fade(Image img, float start, float end, float duration)
        {
            float timer = 0f;
            AnimationCurve smoothCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 1f) });

            while (timer <= duration)
            {
                timer += Time.deltaTime;
                Color c = img.color;
                img.color = new Color(c.r, c.g, c.b, Mathf.Lerp(start, end, smoothCurve.Evaluate(timer / duration)));
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }
};

