using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Coffee.UIEffects;

public class CardsFight : MonoBehaviour
{
    public Music music;
    public Image Overlay;
    public Image PlayerSprite;
    public Image EnemySprite;
    public GameObject PlayerHand;

    [SerializeField]
    private List<Image> fadeImages;

    private UIDissolve dissolve;

    public void Start()
    {
        dissolve = Overlay.GetComponent<UIDissolve>();
        dissolve.effectFactor = 1.0f;

        SetOpacity(0.0f);
        gameObject.SetActive(false);

    }

    public void Open()
    {
        gameObject.SetActive(true);
        GameManager.instance.soundManager.music.Stop(false);
        music.Play();
        StartCoroutine(ShowCoroutine());
    }

    private IEnumerator ShowCoroutine()
    {
        yield return StartCoroutine(Utils.UI.Dissolve(dissolve, 1.0f, 0.0f, 1.0f));
        
        yield return StartCoroutine(Utils.UI.Fade(fadeImages, 0.0f, 1.0f, 0.2f));
    }

    private void SetOpacity(float a)
    {
        foreach (Image img in fadeImages)
            Utils.UI.SetImageOpacity(img, a);
    }
}
