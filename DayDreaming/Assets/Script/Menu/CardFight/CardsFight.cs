using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Coffee.UIEffects;
using TMPro;

public class CardsFight : MonoBehaviour
{
    public Image Overlay;
    public Image PlayerSprite;
    public Image EnemySprite;
    public Image EnemyCard;
    public PlayerHand PlayerHand;
    public Enemy Enemy;

    public ProgressBar PlayerBar;
    public ProgressBar EnemyBar;
    public TextMeshProUGUI TurnText;
    public int Turn;

    [SerializeField]
    private List<Image> fadeImages;
    private List<Image> initFadeImages = new List<Image>();
    private UIDissolve dissolve;

    // For Cards animation
    public List<BezierCurve> CurvesManifold;
    public List<Image> ImagesManifold;


    public void Start()
    {
        dissolve = Overlay.GetComponent<UIDissolve>();
        dissolve.effectFactor = 1.0f;
        foreach (Image img in fadeImages)
            initFadeImages.Add(img);
        fadeImages.Clear();
        SetOpacity(0.0f);
        gameObject.SetActive(false);
    }

    public void Open(Enemy Enemy)
    {

        this.Enemy = Enemy;
        this.Turn = 1;
        gameObject.SetActive(true);
        PlayerHand.LoadCards();
        fadeImages.Clear();
        foreach (Image img in initFadeImages)
            fadeImages.Add(img);
        foreach (UICard card in PlayerHand.UICards)
            fadeImages.Add(card.GetComponent<Image>());
        fadeImages.ForEach(i => Utils.UI.SetImageOpacity(i, 0f));
        PlayerBar.ClearStatusBar();
        EnemyBar.ClearStatusBar();
        UpdateProgressBars();
        GameManager.instance.soundManager.PlayMusic(SoundManager.MusicType.Fight);
        StartCoroutine(ShowCoroutine());
    }

    public void Close()
    {
        GameManager.instance.soundManager.StopMusic(true);
        GameManager.instance.player.status.Clear();
        StartCoroutine(HideCoroutine());
    }


    public void UpdateProgressBars()
    {

        PlayerBar.SetFill(PlayerHand.player.CurrentHP / PlayerHand.player.MaxHP);
        EnemyBar.SetFill(Enemy.CurrentHP / Enemy.MaxHP);
    }

    private IEnumerator ShowCoroutine()
    {
        yield return StartCoroutine(Utils.UI.Dissolve(dissolve, 1.0f, 0.0f, 1.0f));
        
        yield return StartCoroutine(Utils.UI.Fade(fadeImages, 0.0f, 1.0f, 0.2f));
        PlayerBar.SetOpacity(1.0f);
        EnemyBar.SetOpacity(1.0f);
        PlayerHand.player.CanPlay(true);
    }

    private IEnumerator HideCoroutine()
    {
        fadeImages.Clear();
        foreach (UICard card in PlayerHand.UICards)
            fadeImages.Add(card.GetComponent<Image>());

        yield return StartCoroutine(Utils.UI.Fade(fadeImages, 1.0f, 0.0f, 0.2f));
        PlayerBar.SetOpacity(0.0f);
        EnemyBar.SetOpacity(0.0f);
        yield return StartCoroutine(Utils.UI.Fade(new List<Image>() { PlayerSprite, EnemySprite }, 1.0f, 0.0f, 0.5f));

        yield return StartCoroutine(Utils.UI.Dissolve(dissolve, 0.0f, 1.0f, 1.0f));

        GameManager.instance.soundManager.PlayMusic(SoundManager.MusicType.Main);
        gameObject.SetActive(false);
        GameManager.instance.player.isFighting = false;
    }

    public IEnumerator ShowEnemyCard(CardAsset card)
    {
        EnemyCard.sprite = card.Sprite;
        yield return StartCoroutine(Utils.UI.Fade(new List<Image>() { EnemyCard }, 0.0f, 1.0f, 0.5f));
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(Utils.UI.Fade(new List<Image>() { EnemyCard }, 1.0f, 0.0f, 0.5f));
    }

    private void SetOpacity(float a)
    {
        foreach (Image img in fadeImages)
            Utils.UI.SetImageOpacity(img, a);
        PlayerBar.SetOpacity(a);
        EnemyBar.SetOpacity(a);
        TurnText.enabled = a==1f;
    }

    public void EndTurn()
    {
        Turn++;
        TurnText.SetText("" + Turn);
    }
}
